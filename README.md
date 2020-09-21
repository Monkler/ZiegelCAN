# ZiegelCAN

Simple easy to use and cheap CAN GUI + Device

## GUI

![Screenshot](/Dokumentation/GUI-Screenshot.png)

The GUI is written in C# and can be easily modified. Such as adding additinal drivers for different devices.

The GUI supports additinally to the own device following CAN Devices:
- Innomaker: USB zu CAN Konverter Modul (https://www.inno-maker.com/product/usb-can/) (~22€)
- PEAK: PCAN (https://www.peak-system.com/PC-Interfaces.196.0.html?&L=1) (150€ - 400€)

## Device

![Screenshot](/Dokumentation/Device.png)

The device is composed of an arduino (https://www.arduino.cc/) and MC2515 (https://www.microchip.com/wwwproducts/en/en010406) SPI-CAN Module
and can be assembled using different arduino boards / MC2515 modules.

The source code for the arduino can be found at "/Device/ZiegelCAN.ino".

### Assembly examples

*Arduino Pro Micro + MC2515 DevBoard*

Arduino Pro Micro (3€ - 17€):

![Arduino Pro Micro](/Dokumentation/ArduinoProMicro.png)

- [Reichelt](https://www.reichelt.de/arduino-micro-atmega32u4-microusb-arduino-micro-p130166.html?PROVID=2788&gclid=Cj0KCQjwnqH7BRDdARIsACTSAdt4MO3BU86DtwXu6JJwXzTQyJRWxugqHoVLU8AKw7-FK6P4etoctqoaAkZBEALw_wcB&&r=1)
- [Amazon](https://www.amazon.de/s?k=arduino+pro+micro&adgrpid=68574559182&gclid=Cj0KCQjwnqH7BRDdARIsACTSAdsH_4IlpHdsxaQXSTaCFc4L0vXTYrDDimj4OuBm9ByWzXVA2E6BKU8aAofPEALw_wcB&hvadid=352893259388&hvdev=c&hvlocphy=9042646&hvnetw=g&hvqmt=e&hvrand=7661705581012956852&hvtargid=aud-952184766223%3Akwd-298203302135&hydadcr=20073_1780756&tag=googhydr08-21&ref=pd_sl_2zvjlp4hz1_e)

MC2515 Devboard (~6€):

![MC2515 Devboard](/Dokumentation/MC2515Devboard.png)

- [Reichelt](https://www.reichelt.de/entwicklerboards-can-modul-mcp2515-mcp2562-debo-can-modul-p239277.html?PROVID=2788&gclid=Cj0KCQjwnqH7BRDdARIsACTSAduSsxQXZH1qjitB8yJM9Uq9lB601zxAiicDDWQApFdCmpEz34VcJ7oaAtaXEALw_wcB&&r=1)
- [Conrad](https://www.conrad.de/de/p/joy-it-sbc-can01-can-interface-1-st-passend-fuer-arduino-banana-pi-raspberry-pi-cubieboard-1720599.html?hk=SEM&WT.srch=1&WT.mc_id=google_pla&s_kwcid=AL%21222%213%21367270211499%21%21%21g%21%21&ef_id=Cj0KCQjwnqH7BRDdARIsACTSAdtjLrU0uN2fT1p9jbrQR8k4eKA0wHKeFqM9jfXznG4Rc9zTmOjwDoUaArQVEALw_wcB%3AG%3As&gclid=Cj0KCQjwnqH7BRDdARIsACTSAdtjLrU0uN2fT1p9jbrQR8k4eKA0wHKeFqM9jfXznG4Rc9zTmOjwDoUaArQVEALw_wcB&refresh=true)
- [Amazon](https://www.amazon.de/AptoFun-Receiver-Protocol-Controller-Development/dp/B0758VD6WR/ref=sr_1_1_sspa?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&dchild=1&keywords=arduino+can+modul&qid=1600681236&sr=8-1-spons&psc=1&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUFJR0E4STJGRlQ3VUcmZW5jcnlwdGVkSWQ9QTAyMzUwOTcyRzU4R0owUzZOWVdEJmVuY3J5cHRlZEFkSWQ9QTA1NjQxNjY2MFhNNUxMQVc5OVImd2lkZ2V0TmFtZT1zcF9hdGYmYWN0aW9uPWNsaWNrUmVkaXJlY3QmZG9Ob3RMb2dDbGljaz10cnVl)

Schematic:

![Schematic](/Dokumentation/Device-ArduinoProMicro-Schematic.png)

![Picture](/Dokumentation/Device-ArduinoProMicro-Picture.png)

3D-Print housing:

 - See "/Device/ArduinoProMicro-Housing.sdl"
 
![Housing](/Dokumentation/Device-ArduinoProMicro-Housing.png)

*Arduino Beetle + MC2515 DevBoard*

Arduino Beetle (~9€):

![Arduino Beetle](/Dokumentation/ArduinoBeetle.png)

- [Amazon](https://www.amazon.de/DollaTek-ATMEGA32U4-USB-Entwicklungsplatinenmodul-kompatibel-Leonardo/dp/B07ML2RX59/ref=sr_1_2?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&crid=14238SW0479DR&dchild=1&keywords=Arduino+Beetle&qid=1600682161&quartzVehicle=121-634&replacementKeywords=arduino&sprefix=philips+am%2Caps%2C168&sr=8-2)

MC2515 Devboard (~6€):

![MC2515 Devboard](/Dokumentation/MC2515Devboard.png)

- [Reichelt](https://www.reichelt.de/entwicklerboards-can-modul-mcp2515-mcp2562-debo-can-modul-p239277.html?PROVID=2788&gclid=Cj0KCQjwnqH7BRDdARIsACTSAduSsxQXZH1qjitB8yJM9Uq9lB601zxAiicDDWQApFdCmpEz34VcJ7oaAtaXEALw_wcB&&r=1)
- [Conrad](https://www.conrad.de/de/p/joy-it-sbc-can01-can-interface-1-st-passend-fuer-arduino-banana-pi-raspberry-pi-cubieboard-1720599.html?hk=SEM&WT.srch=1&WT.mc_id=google_pla&s_kwcid=AL%21222%213%21367270211499%21%21%21g%21%21&ef_id=Cj0KCQjwnqH7BRDdARIsACTSAdtjLrU0uN2fT1p9jbrQR8k4eKA0wHKeFqM9jfXznG4Rc9zTmOjwDoUaArQVEALw_wcB%3AG%3As&gclid=Cj0KCQjwnqH7BRDdARIsACTSAdtjLrU0uN2fT1p9jbrQR8k4eKA0wHKeFqM9jfXznG4Rc9zTmOjwDoUaArQVEALw_wcB&refresh=true)
- [Amazon](https://www.amazon.de/AptoFun-Receiver-Protocol-Controller-Development/dp/B0758VD6WR/ref=sr_1_1_sspa?__mk_de_DE=%C3%85M%C3%85%C5%BD%C3%95%C3%91&dchild=1&keywords=arduino+can+modul&qid=1600681236&sr=8-1-spons&psc=1&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUFJR0E4STJGRlQ3VUcmZW5jcnlwdGVkSWQ9QTAyMzUwOTcyRzU4R0owUzZOWVdEJmVuY3J5cHRlZEFkSWQ9QTA1NjQxNjY2MFhNNUxMQVc5OVImd2lkZ2V0TmFtZT1zcF9hdGYmYWN0aW9uPWNsaWNrUmVkaXJlY3QmZG9Ob3RMb2dDbGljaz10cnVl)

Schematic:

![Schematic](/Dokumentation/Device-ArduinoBeetle-Schematic.png)

![Picture](/Dokumentation/Device-ArduinoBeetle-Picture.png)

3D-Print housing:

 - See "/Device/ArduinoBeetle-Housing.sdl"
