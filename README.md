# ZiegelCAN

Simple easy to use and cheap CAN GUI + Device

## GUI

![Screenshot](/Dokumentation/GUI-Screenshot.png)

The GUI is written in C# .NET and can be easily modified. Such as adding additinal drivers for different devices.

Currently supported devices:
- ZiegelCAN: Based on Arduino and MC2515
- Innomaker: USB zu CAN Konverter Modul (https://www.inno-maker.com/product/usb-can/) (~22€)
  - [Amazon](https://www.amazon.de/USB-CAN-Konvertermodul-f%C3%BCr-Raspberry-Zero/dp/B07Q812QK8/ref=sr_1_1?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&dchild=1&keywords=usb+to+can&qid=1600685248&s=computers&sr=1-1)
- PEAK: PCAN (https://www.peak-system.com/PC-Interfaces.196.0.html?&L=1) (150€ - 400€)

## Device

![Screenshot](/Dokumentation/Device.png)

The device is composed of an arduino (https://www.arduino.cc/) and MC2515 (https://www.microchip.com/wwwproducts/en/en010406) SPI-CAN Module
and can be assembled using different arduino boards / MC2515 modules.

The source code for the arduino can be found at "/Device/ZiegelCAN.ino".

The device is communicating very simple via the serial bus to the master and can be simply controlled via a serial terminal (e.g. [Putty](https://www.putty.org/)).
Following commands are supported:
<table>
 <tr>
  <td>Name</td>
  <td>Template</td>
  <td>Example</td>
  <td>Description</td>
 </tr>
 <tr>
  <td>Initalize</td>
  <td><code>I 'Baudrate'</code></td>
  <td><code>I 16</code></td>
  <td>
   Will be done by default on power up with Baudrate=500k<br />
   <details>
    <summary>Supported values:</summary>
    <ul>
     <li> 1: 5K BPS </li>
     <li> 2: 10KBPS </li>
     <li> 3: CAN_20KBPS </li>
     <li> 4: CAN_25KBPS </li>
     <li> 5: CAN_31K25BPS </li>
     <li> 6: CAN_33KBPS </li>
     <li> 7: CAN_40KBPS </li>
     <li> 8: CAN_50KBPS </li>
     <li> 9: CAN_80KBPS </li>
     <li> 10: CAN_83K3BPS </li>
     <li> 11: CAN_95KBPS </li>
     <li> 12: CAN_100KBPS </li>
     <li> 13: CAN_125KBPS </li>
     <li> 14: CAN_200KBPS </li>
     <li> 15: CAN_250KBPS </li>
     <li> 16: CAN_500KBPS </li>
     <li> 17: CAN_666KBPS </li>
     <li> 18: CAN_1000KBPS </li>
    </ul>
   </detail>
  </td>
 </tr>
 <tr>
  <td>Write</td>
  <td><code>W 'id' 'length' 'data'</code></td>
  <td><code>W 34 05 123456789A</code></td>
  <td>Sends an CAN Message. <br />
   ID in 2 digit hex format <br />
   Length in 2 digit hex format <br />
   Data in 2 digit hex format per byte<br />
   Will return <code>W 'timestamp' 'id' 'length' 'data'</code><br />
   e.g. <code>W 0000388D 34 05 123456789A</code><br />
  </td>
 </tr>
  <tr>
  <td>Read</td>
  <td><code>R 'timestamp' 'id' 'length' 'data'</code></td>
  <td><code>W 0000388D 34 05 123456789A</code></td>
  <td>Occures automatically on receive of a CAN message. <br />
   Timestamp in 2 digit hex format <br />
   ID in 8 digit hex format (milliseconds relative to start time of device) <br />   
   Length in 2 digit hex format <br />
   Data in 2 digit hex format per byte<br />
   <code>D 'timestamp' 'id' 'length' 'data'</code><br />
   e.g. <code>W 0000388D 34 05 123456789A</code><br />
  </td>
 </tr>
</table> 

### Assembly examples

**Arduino Pro Micro + MC2515 DevBoard**

Arduino Pro Micro (3€ - 17€):

![Arduino Pro Micro](/Dokumentation/ArduinoProMicro.jpg)

- [Reichelt](https://www.reichelt.de/arduino-micro-atmega32u4-microusb-arduino-micro-p130166.html?PROVID=2788&gclid=Cj0KCQjwnqH7BRDdARIsACTSAdt4MO3BU86DtwXu6JJwXzTQyJRWxugqHoVLU8AKw7-FK6P4etoctqoaAkZBEALw_wcB&&r=1)
- [Amazon](https://www.amazon.de/s?k=arduino+pro+micro&adgrpid=68574559182&gclid=Cj0KCQjwnqH7BRDdARIsACTSAdsH_4IlpHdsxaQXSTaCFc4L0vXTYrDDimj4OuBm9ByWzXVA2E6BKU8aAofPEALw_wcB&hvadid=352893259388&hvdev=c&hvlocphy=9042646&hvnetw=g&hvqmt=e&hvrand=7661705581012956852&hvtargid=aud-952184766223%3Akwd-298203302135&hydadcr=20073_1780756&tag=googhydr08-21&ref=pd_sl_2zvjlp4hz1_e)

MC2515 Devboard (~6€):

![MC2515 Devboard](/Dokumentation/MC2515Devboard.jpg)

- [Reichelt](https://www.reichelt.de/entwicklerboards-can-modul-mcp2515-mcp2562-debo-can-modul-p239277.html?PROVID=2788&gclid=Cj0KCQjwnqH7BRDdARIsACTSAduSsxQXZH1qjitB8yJM9Uq9lB601zxAiicDDWQApFdCmpEz34VcJ7oaAtaXEALw_wcB&&r=1)
- [Conrad](https://www.conrad.de/de/p/joy-it-sbc-can01-can-interface-1-st-passend-fuer-arduino-banana-pi-raspberry-pi-cubieboard-1720599.html?hk=SEM&WT.srch=1&WT.mc_id=google_pla&s_kwcid=AL%21222%213%21367270211499%21%21%21g%21%21&ef_id=Cj0KCQjwnqH7BRDdARIsACTSAdtjLrU0uN2fT1p9jbrQR8k4eKA0wHKeFqM9jfXznG4Rc9zTmOjwDoUaArQVEALw_wcB%3AG%3As&gclid=Cj0KCQjwnqH7BRDdARIsACTSAdtjLrU0uN2fT1p9jbrQR8k4eKA0wHKeFqM9jfXznG4Rc9zTmOjwDoUaArQVEALw_wcB&refresh=true)
- [Amazon](https://www.amazon.de/AptoFun-Receiver-Protocol-Controller-Development/dp/B0758VD6WR/ref=sr_1_1_sspa?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&dchild=1&keywords=arduino+can+modul&qid=1600681236&sr=8-1-spons&psc=1&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUFJR0E4STJGRlQ3VUcmZW5jcnlwdGVkSWQ9QTAyMzUwOTcyRzU4R0owUzZOWVdEJmVuY3J5cHRlZEFkSWQ9QTA1NjQxNjY2MFhNNUxMQVc5OVImd2lkZ2V0TmFtZT1zcF9hdGYmYWN0aW9uPWNsaWNrUmVkaXJlY3QmZG9Ob3RMb2dDbGljaz10cnVl)

Schematic:

<img src="/Dokumentation/Device-ArduinoProMicro-Picture.png" width="300">
<img src="/Dokumentation/Device-ArduinoProMicro-Schematic.png" width="300">

3D-Print housing:

 - See "/Device/ArduinoProMicro-Housing.sdl"
 
![Housing](/Dokumentation/Device-ArduinoProMicro-Housing.png)

**Arduino Beetle + MC2515 DevBoard**

Arduino Beetle (~9€):

![Arduino Beetle](/Dokumentation/ArduinoBeetle.jpg)

- [Amazon](https://www.amazon.de/DollaTek-ATMEGA32U4-USB-Entwicklungsplatinenmodul-kompatibel-Leonardo/dp/B07ML2RX59/ref=sr_1_2?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&crid=14238SW0479DR&dchild=1&keywords=Arduino+Beetle&qid=1600682161&quartzVehicle=121-634&replacementKeywords=arduino&sprefix=philips+am%2Caps%2C168&sr=8-2)

MC2515 Devboard (~6€):

![MC2515 Devboard](/Dokumentation/MC2515Devboard.jpg)

- [Reichelt](https://www.reichelt.de/entwicklerboards-can-modul-mcp2515-mcp2562-debo-can-modul-p239277.html?PROVID=2788&gclid=Cj0KCQjwnqH7BRDdARIsACTSAduSsxQXZH1qjitB8yJM9Uq9lB601zxAiicDDWQApFdCmpEz34VcJ7oaAtaXEALw_wcB&&r=1)
- [Conrad](https://www.conrad.de/de/p/joy-it-sbc-can01-can-interface-1-st-passend-fuer-arduino-banana-pi-raspberry-pi-cubieboard-1720599.html?hk=SEM&WT.srch=1&WT.mc_id=google_pla&s_kwcid=AL%21222%213%21367270211499%21%21%21g%21%21&ef_id=Cj0KCQjwnqH7BRDdARIsACTSAdtjLrU0uN2fT1p9jbrQR8k4eKA0wHKeFqM9jfXznG4Rc9zTmOjwDoUaArQVEALw_wcB%3AG%3As&gclid=Cj0KCQjwnqH7BRDdARIsACTSAdtjLrU0uN2fT1p9jbrQR8k4eKA0wHKeFqM9jfXznG4Rc9zTmOjwDoUaArQVEALw_wcB&refresh=true)
- [Amazon](https://www.amazon.de/AptoFun-Receiver-Protocol-Controller-Development/dp/B0758VD6WR/ref=sr_1_1_sspa?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&dchild=1&keywords=arduino+can+modul&qid=1600681236&sr=8-1-spons&psc=1&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUFJR0E4STJGRlQ3VUcmZW5jcnlwdGVkSWQ9QTAyMzUwOTcyRzU4R0owUzZOWVdEJmVuY3J5cHRlZEFkSWQ9QTA1NjQxNjY2MFhNNUxMQVc5OVImd2lkZ2V0TmFtZT1zcF9hdGYmYWN0aW9uPWNsaWNrUmVkaXJlY3QmZG9Ob3RMb2dDbGljaz10cnVl)

Schematic:

<img src="/Dokumentation/Device-ArduinoBeetle-Picture.png" width="300">
<img src="/Dokumentation/Device-ArduinoBeetle-Schematic.png" width="300">

3D-Print housing:

 - See "/Device/ArduinoBeetle-Housing.sdl"
 
 ![Housing](/Dokumentation/Device-ArduinoBeetle-Housing.png)
