#define DEBUG_EN        0

#include <SPI.h>
#include <mcp2515.h>

#define SPI_CS_PIN 10
#define LED_PIN 13

MCP2515 mcp2515(SPI_CS_PIN);

bool isInit = false;

void setup() {
  Serial.begin(115200);
  pinMode(LED_PIN, OUTPUT);
  digitalWrite(LED_PIN, 0);

  mcp2515.reset();
  if (MCP2515::ERROR::ERROR_OK != mcp2515.setBitrate(CAN_500KBPS, MCP_8MHZ)) { 
    Serial.println("I BAD");          
  }
  else {
    Serial.println("I OK");
    digitalWrite(LED_PIN, 1);
    isInit = true;
  }

  if (MCP2515::ERROR::ERROR_OK != mcp2515.setNormalMode()) { 
    Serial.println("M BAD");        
  }
  else {
    Serial.println("M OK");
    digitalWrite(LED_PIN, 1);
  }
}

void printTimestamp() {
  String timestamp = String(millis(), HEX);
  for (int i = 0; i < 8 - timestamp.length(); i++) {
    Serial.print("0");
  }
  Serial.print(timestamp);
}

void printCharAsHex(unsigned char value) {
  if (value <= 15) {
    Serial.print("0");
  }

  Serial.print(String((byte)(value), HEX));
}

void printMessage(unsigned int canId, unsigned char len, unsigned char* buf) {  
  if (canId <= 255) {
    Serial.print("0");
  }

  if (canId <= 15) {
    Serial.print("0");
  }

  Serial.print(String(canId, HEX));

  Serial.print(" ");
  printCharAsHex(len);
  Serial.print(" ");

  for (int i = 0; i < len; i++) {
    printCharAsHex(buf[i]);
  }

  for (int i = len; i < 8; i++) {
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

struct can_frame sendCanMsg; 
struct can_frame receiveCanMsg[30];
int receiveCanMsgIndicator =  0;

void loop() {
  if (Serial.available()) {
    String msg = Serial.readStringUntil('\r');

    if (msg.startsWith("I")) {
      digitalWrite(LED_PIN, 0);      

      mcp2515.reset();
      if (MCP2515::ERROR::ERROR_OK != mcp2515.setBitrate((enum CAN_SPEED)msg.substring(2).toInt(), MCP_8MHZ)) { 
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
      
      String mode = msg.substring(2);
      if (mode == "1") {
        if (MCP2515::ERROR::ERROR_OK != mcp2515.setNormalMode()) { 
          Serial.println("M BAD");        
        }
        else {
          Serial.println("M OK");
          digitalWrite(LED_PIN, 1);
        }
      }
      else if (mode == "2") {
       if (MCP2515::ERROR::ERROR_OK != mcp2515.setListenOnlyMode()) { 
          Serial.println("M BAD");        
        }
        else {
          Serial.println("M OK");
          digitalWrite(LED_PIN, 1);
        }
      }
      else if (mode == "3") {
       if (MCP2515::ERROR::ERROR_OK != mcp2515.setLoopbackMode()) { 
          Serial.println("M BAD");        
        }
        else {
          Serial.println("M OK");
          digitalWrite(LED_PIN, 1);
        }
      }
      else {
        Serial.println("M BAD");   
      }
    }
    else if (msg.startsWith("W") && msg.length() >= 24) {
      digitalWrite(LED_PIN, 0);     

      const char *msgStr = msg.c_str();

      sendCanMsg.can_id = hex2int(msgStr[2]) << 8 | hex2int(msgStr[3]) << 4 | hex2int(msgStr[4]);
      if (sendCanMsg.can_id > 2047) {
        sendCanMsg.can_id = 2048;
      }

      sendCanMsg.can_dlc = hex2int(msgStr[6]) << 4 | hex2int(msgStr[7]);
      if (sendCanMsg.can_dlc > 8) {
        sendCanMsg.can_dlc = 8;
      }

      sendCanMsg.data[0] = hex2int(msgStr[9]) << 4 | hex2int(msgStr[10]);
      sendCanMsg.data[1] = hex2int(msgStr[11]) << 4 | hex2int(msgStr[12]);
      sendCanMsg.data[2] = hex2int(msgStr[13]) << 4 | hex2int(msgStr[14]);
      sendCanMsg.data[3] = hex2int(msgStr[15]) << 4 | hex2int(msgStr[16]);
      sendCanMsg.data[4] = hex2int(msgStr[17]) << 4 | hex2int(msgStr[18]);
      sendCanMsg.data[5] = hex2int(msgStr[19]) << 4 | hex2int(msgStr[20]);
      sendCanMsg.data[6] = hex2int(msgStr[21]) << 4 | hex2int(msgStr[22]);
      sendCanMsg.data[7] = hex2int(msgStr[23]) << 4 | hex2int(msgStr[24]);
      
      mcp2515.sendMessage(&sendCanMsg);

      Serial.print("W ");
      printTimestamp();
      Serial.print(" ");
      printMessage((unsigned int)sendCanMsg.can_id, sendCanMsg.can_dlc, sendCanMsg.data);
      Serial.println();
      
      digitalWrite(LED_PIN, 1);
    }
    else {
      Serial.println("X Unkown");
    }
  }  

  while (isInit && mcp2515.readMessage(&(receiveCanMsg[receiveCanMsgIndicator++])) == MCP2515::ERROR_OK) 
  {
    if (sendCanMsg.can_id > 2047) {
      sendCanMsg.can_id = 2048;
    }

    if (receiveCanMsgIndicator >= 20) {
      break;
    }
  }

  for (int i = 0; i < receiveCanMsgIndicator; i++) {
    Serial.print("R ");
    printTimestamp();
    Serial.print(" ");
    printMessage((unsigned int)receiveCanMsg[i].can_id, receiveCanMsg[i].can_dlc, receiveCanMsg[i].data);
    Serial.println();
  }

  receiveCanMsgIndicator = 0;
}