namespace ZiegelCAN
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public partial class MainWindow : Window
    {
        private bool isConnected = false;

        private ObservableCollection<Driver> drivers;
        private Driver selectedDriver;

        ObservableCollection<Message>[] messages;
        MessageInformation[] informations;

        public bool IsConnected { get => isConnected; }

        public ObservableCollection<Driver> Drivers { get => drivers; }
        public Driver SelectedDriver { get => selectedDriver; set => selectedDriver = value; }

        public ObservableCollection<Message>[] Messages { get => messages; }
        public MessageInformation[] Informations { get => informations; }

        public MainWindow()
        {
            this.drivers = new ObservableCollection<Driver>();            

            this.messages = new ObservableCollection<Message>[255];
            for (int i = 0; i < this.messages.Length; i++) 
            {
                this.messages[i] = new ObservableCollection<Message>();
            }

            this.informations = null;

            this.DataContext = this;

            InitializeComponent();

            if (this.Height > SystemParameters.PrimaryScreenHeight * 0.66)
                this.Height = SystemParameters.PrimaryScreenHeight * 0.66;
            if (this.Width > SystemParameters.PrimaryScreenWidth * 0.55)
                this.Width = SystemParameters.PrimaryScreenWidth * 0.55;

            this.InitDrivers();
        }

        private void InitDrivers()
        {
            this.drivers.Clear();

            this.drivers.Add(new Simulator());
            this.drivers.Add(new ZiegelCANDriver());
            this.drivers.Add(new PeakDriver());

            if (this.drivers.Count != 0)
            {
                this.selectedDriver = this.drivers[0];
            }
            else
            {
                this.selectedDriver = null;
            }

            this.DriverSelection.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateTarget();
        }

        private void DriverSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.ChannelSelection.GetBindingExpression(ComboBox.ItemsSourceProperty).UpdateTarget();
            this.ChannelSelection.GetBindingExpression(ComboBox.SelectedItemProperty).UpdateTarget();

            this.ChannelSelection.IsEnabled = (this.selectedDriver != null && !this.isConnected);
        }

        private void ChannelSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ConnectButton.IsEnabled = (this.selectedDriver != null && this.selectedDriver.SelectedChannel != null);
        }

        private void OnLoadDBCFileClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented yet!");
            //this.informations = new MessageInformation[4];
            //this.MessageListView.GetBindingExpression(MessageList.InformationsProperty).UpdateTarget();
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs e)
        {
            this.InitDrivers();
        }

        private void OnConnectButtonClick(object sender, RoutedEventArgs e)
        {           

            if (this.isConnected == false)
            {
                this.isConnected = true;
                this.selectedDriver.ErrorOccured += OnSelectedDriverErrorOccured;
                this.selectedDriver.NewMessage += OnSelectedDriverNewMessage;
                this.selectedDriver.Connect();
            }
            else
            {
                this.isConnected = false;
                this.selectedDriver.ErrorOccured -= OnSelectedDriverErrorOccured;
                this.selectedDriver.NewMessage -= OnSelectedDriverNewMessage;
                this.selectedDriver.Disconnect();
            }
            
            this.SetConnectButtonImage();
        }

        private void OnSelectedDriverErrorOccured(object sender, ErrorEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.isConnected = false;
                this.selectedDriver.ErrorOccured -= OnSelectedDriverErrorOccured;
                this.selectedDriver.NewMessage += OnSelectedDriverNewMessage;
                this.selectedDriver.Disconnect();                

                ExceptionDialog.ShowDialog(e.GetException(), "Error", "Error occured in Driver");

                this.SetConnectButtonImage();
            });
        }

        private void OnSelectedDriverNewMessage(object sender, NewMessageEventArgs e)
        {
            this.messages[e.Message.Id].Add(e.Message);
        }

        private void SetConnectButtonImage()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            if (this.isConnected)
            {
                image.UriSource = new Uri("pack://application:,,,/Resources/Icon/Disconnect.png");
            }
            else
            {
                image.UriSource = new Uri("pack://application:,,,/Resources/Icon/Connect.png");
            }
            image.EndInit();

            ((Image)this.ConnectButton.Content).Source = image;

            this.RefreshButton.IsEnabled = !this.isConnected;
            this.DriverSelection.IsEnabled = !this.isConnected;
            this.ChannelSelection.IsEnabled = (this.selectedDriver != null && !this.isConnected);
        }

        private void OnClearButtonClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.messages.Length; i++)
            {
                this.messages[i].Clear();
            }
        }
    }
}
