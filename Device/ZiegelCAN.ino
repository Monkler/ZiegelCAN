#define DEBUG_EN        0

#include <SPI.h>
#include <mcp_can.h>

#define SPI_CS_PIN 10
#define LED_PIN 13

MCP_CAN CAN(SPI_CS_PIN);

bool isInit = false;

void setup() {
  Serial.begin(115200);
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, 0);

  if (CAN_OK != CAN.begin(CAN_500KBPS)) { 
      Serial.println("I BAD");          
  }
  else {
    Serial.println("I OK");
    digitalWrite(LED_PIN, 1);
    isInit = true;
  }
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

void printMessage(unsigned int* canId, unsigned char* len, unsigned char* buf) {  
  if (*canId <= 255) {
    Serial.print("0");
  }

  if (*canId <= 15) {
    Serial.print("0");
  }

  Serial.print(String(*canId, HEX));

  Serial.print(" ");
  printCharAsHex(len);
  Serial.print(" ");

  for (int i = 0; i < *len; i++) {
    printCharAsHex(&(buf[i]));
  }

  for (int i = *len; i < 8; i++) {
    Serial.print("00");
  }
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
          isInit = false;         
      }
      else {
        Serial.println("I OK");
        digitalWrite(LED_PIN, 1);
        isInit = true;
      }
    }
    else if (msg.startsWith("M")) {
      digitalWrite(LED_PIN, 0);
      

      if (CAN_OK != CAN.setMode((byte)msg.substring(2).toInt())) { 
          Serial.println("M BAD");          
      }
      else {
        Serial.println("M OK");
        digitalWrite(LED_PIN, 1);
      }
    }
    else if (msg.startsWith("W") && msg.length() >= 24) {
      digitalWrite(LED_PIN, 0);     

      const char *msgStr = msg.c_str();
      unsigned int canId = hex2int(msgStr[2]) << 8 | hex2int(msgStr[3]) << 4 | hex2int(msgStr[4]);
      unsigned char len = hex2int(msgStr[6]) << 4 | hex2int(msgStr[7]);
      unsigned char buf[8];
      buf[0] = hex2int(msgStr[9]) << 4 | hex2int(msgStr[10]);
      buf[1] = hex2int(msgStr[11]) << 4 | hex2int(msgStr[12]);
      buf[2] = hex2int(msgStr[13]) << 4 | hex2int(msgStr[14]);
      buf[3] = hex2int(msgStr[15]) << 4 | hex2int(msgStr[16]);
      buf[4] = hex2int(msgStr[17]) << 4 | hex2int(msgStr[18]);
      buf[5] = hex2int(msgStr[19]) << 4 | hex2int(msgStr[20]);
      buf[6] = hex2int(msgStr[21]) << 4 | hex2int(msgStr[22]);
      buf[7] = hex2int(msgStr[23]) << 4 | hex2int(msgStr[24]);

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

  if (isInit && CAN_MSGAVAIL == CAN.checkReceive())
  {
    unsigned char len = 0;
    unsigned char buf[8];
    CAN.readMsgBuf(&len, buf);

    unsigned int canId = CAN.getCanId();

    Serial.print("R ");
    printTimestamp();
    Serial.print(" ");
    printMessage(&canId, &len, buf);
    Serial.println();
  }
}