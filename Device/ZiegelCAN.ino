#define DEBUG_EN        0

#include <SPI.h>
#include <mcp_can.h>

#define SPI_CS_PIN 10
#define LED_PIN 13

MCP_CAN CAN(SPI_CS_PIN);

void setup() {
  Serial.begin(115200);
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, 0);
}

void printTimestamp() {
  String timestamp = String(millis(), HEX);
  for (int i = 0; i < 8 - timestamp.length(); i++) {
    Serial.print("0");
  }
  Serial.print(timestamp);
}

void printCharAsHex(unsigned char* value) {
  if (*value <= 15) {
    Serial.print("0");
  }

  Serial.print(String((byte)(*value), HEX));
}

void printMessage(unsigned char* canId, unsigned char* len, unsigned char* buf) {
  printCharAsHex(canId);
  Serial.print(" ");
  printCharAsHex(len);
  Serial.print(" ");
  printCharAsHex(&(buf[0]));
  printCharAsHex(&(buf[1]));
  printCharAsHex(&(buf[2]));
  printCharAsHex(&(buf[3]));
  printCharAsHex(&(buf[4]));
  printCharAsHex(&(buf[5]));
  printCharAsHex(&(buf[6]));
  printCharAsHex(&(buf[7]));
}

int hex2int(char ch)
{
    if (ch >= '0' && ch <= '9')
        return ch - '0';
    if (ch >= 'A' && ch <= 'F')
        return ch - 'A' + 10;
    if (ch >= 'a' && ch <= 'f')
        return ch - 'a' + 10;
    return -1;
}

void loop() {
  if (Serial.available()) {
    String msg = Serial.readStringUntil('\r');

    if (msg.startsWith("I")) {
      digitalWrite(LED_PIN, 0);

      if (CAN_OK != CAN.begin(msg.substring(2).toInt())) { // CAN_500KBPS
          Serial.println("I BAD");          
      }
      else {
        Serial.println("I OK");
        digitalWrite(LED_PIN, 1);
      }
    }
    else if (msg.startsWith("M")) {
      digitalWrite(LED_PIN, 0);
      CAN.setMode((byte)msg.substring(2).toInt());
      digitalWrite(LED_PIN, 1);
    }
    else if (msg.startsWith("W") && msg.length() >= 24) {
      digitalWrite(LED_PIN, 0);     

      const char *msgStr = msg.c_str();
      unsigned char canId = hex2int(msgStr[2]) << 4 | hex2int(msgStr[3]);
      unsigned char len = hex2int(msgStr[5]) << 4 | hex2int(msgStr[6]);
      unsigned char buf[8];
      buf[0] = hex2int(msgStr[8]) << 4 | hex2int(msgStr[9]);
      buf[1] = hex2int(msgStr[10]) << 4 | hex2int(msgStr[11]);
      buf[2] = hex2int(msgStr[12]) << 4 | hex2int(msgStr[13]);
      buf[3] = hex2int(msgStr[14]) << 4 | hex2int(msgStr[15]);
      buf[4] = hex2int(msgStr[16]) << 4 | hex2int(msgStr[17]);
      buf[5] = hex2int(msgStr[18]) << 4 | hex2int(msgStr[19]);
      buf[6] = hex2int(msgStr[20]) << 4 | hex2int(msgStr[21]);
      buf[7] = hex2int(msgStr[22]) << 4 | hex2int(msgStr[23]);

      CAN.sendMsgBuf(canId, 0, len, buf, false);

      Serial.print("W ");
      printTimestamp();
      Serial.print(" ");
      printMessage(&canId, &len, buf);
      Serial.println();
      
      digitalWrite(LED_PIN, 1);
    }
    else {
      Serial.println("X Unkown");
    }
  }

  if (CAN_MSGAVAIL == CAN.checkReceive())
  {
    unsigned char len = 0;
    unsigned char buf[8];
    CAN.readMsgBuf(&len, buf);

    unsigned char canId = CAN.getCanId();

    Serial.print("R ");
    printTimestamp();
    Serial.print(" ");
    printMessage(&canId, &len, buf);
    Serial.println();
  }
}