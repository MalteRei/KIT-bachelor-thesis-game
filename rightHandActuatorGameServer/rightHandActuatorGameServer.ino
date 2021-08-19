#include <WiFi.h>
#include <WiFiClient.h>
#include <WebServer.h>
#include <ESPmDNS.h>


#define LEFT_INPUT_1 18
#define LEFT_INPUT_2 19

#define RIGHT_INPUT_1 16
#define RIGHT_INPUT_2 17


double tap_freq = 7;
double interval = (1.0/(tap_freq * 2))*1000.0;
double tap_duration = 300;
double number_of_tap_iterations = tap_duration / interval;

int zero = 0;

const TickType_t WaitTime = 5 / portTICK_PERIOD_MS;


WebServer server(80);

class Actuator {
  public:
    Actuator(){}
    Actuator(int gpio_pin_actuator_1, int gpio_pin_actuator_2)
      : gpio_pin_1(gpio_pin_actuator_1), gpio_pin_2(gpio_pin_actuator_2)
      {
        
   pinMode(gpio_pin_1, OUTPUT);
  pinMode(gpio_pin_2, OUTPUT);
        }
  
  

    void play(int input_1, int input_2 ){
       digitalWrite(gpio_pin_1, input_1);
       digitalWrite(gpio_pin_2, input_2);
    }

    

    private:
      int gpio_pin_1 {0};
      int gpio_pin_2 {0};
      
      
    
      
};


/*
const char* ssid = "Galaxy A50DF27";
const char* password = "uegm8153";
*/
const char* ssid = "UPCE316243";
const char* password = "Defender!2007_2005";



void handleNotFound() {
  String message = "Not Found\n\n";
  message += "URI: ";
  message += server.uri();
  message += "\nMethod: ";
  message += (server.method() == HTTP_GET) ? "GET" : "POST";
  message += "\nArguments: ";
  message += server.args();
  message += "\n";
  for (uint8_t i = 0; i < server.args(); i++) {
    message += " " + server.argName(i) + ": " + server.arg(i) + "\n";
  }
  server.send(404, "text/plain", message);
}

typedef enum  {
  None, Fast, Normal, Slow
}GameVibrationPattern;

GameVibrationPattern currentPatternToPlay;
bool isPlaying = false;


void playActuators(void * pvParameters){
    GameVibrationPattern previousPatternPlayed = currentPatternToPlay;

    Actuator left {LEFT_INPUT_1, LEFT_INPUT_2};
    Actuator right {RIGHT_INPUT_1, RIGHT_INPUT_2};

    Actuator *toPlay = &right;
    
    for(;;){
      if(currentPatternToPlay != previousPatternPlayed){
        previousPatternPlayed = currentPatternToPlay;
        Actuator *toPlay = &right;
      }
      if(!isPlaying){
        right.play(LOW, LOW);
          left.play(LOW, LOW);
          continue;
      }
      switch(currentPatternToPlay) {
        case Normal:
            delay(20);
            toPlay->play(LOW, HIGH);
        
            delay(180);
            toPlay->play(HIGH, LOW);
            
            delay(800);
            break;
        case Fast:
           /* delay(100);
            toPlay->play(LOW, HIGH);
            
            delay(100);
             toPlay->play(HIGH, LOW);
  
             delay(800);
             break;*/
             delay(10);
            toPlay->play(LOW, HIGH);
            
            delay(10);
             toPlay->play(HIGH, LOW);
  
             delay(80);
             break;

         case Slow:
            delay(200);
            toPlay->play(LOW, HIGH);
            
            delay(50);
             toPlay->play(HIGH, LOW);
  
             delay(750);
             break;

             
        
            
          
      }
      if(toPlay == &right){
        toPlay = &left;
      } else {
        toPlay = &right;
      }
  }
  Serial.println("done");
}


 void createTaskPlayingActuators(){
      Serial.println("create task");
  TaskHandle_t xHandle = NULL;
      xTaskCreatePinnedToCore(
          playActuators, /* Function to implement the task */
          "Task1", /* Name of the task */
          10000,  /* Stack size in words */
          NULL, /* Task input parameter */
          0,  /* Priority of the task */
          &xHandle,  /* Task handle. */
          0); /* Core where the task should run */

            Serial.println("created task");
   
 }

void setup(void) {
  Serial.begin(115200);



 createTaskPlayingActuators();
  
  
  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  Serial.println("");

  // Wait for connection
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected to ");
  Serial.println(ssid);
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());

  if (MDNS.begin("esp32")) {
    Serial.println("MDNS responder started");
  }

  server.on("/hand/right/vibration/set/normal", HTTP_POST, [](){
    currentPatternToPlay = Normal;
    server.send(202, "text/plain", "set pattern to normal");
  });

  server.on("/hand/right/vibration/set/fast", HTTP_POST, [](){
    currentPatternToPlay = Fast;
    server.send(202, "text/plain", "set pattern to fast");
  });

  server.on("/hand/right/vibration/set/slow", HTTP_POST, [](){
    currentPatternToPlay = Slow;
   server.send(202, "text/plain", "set pattern to slow");
  });

  server.on("/hand/right/vibration/stop", HTTP_POST, [](){
    if(isPlaying){
      isPlaying = false;
      server.send(202, "text/plain", "stopped playing");
    } else {
      server.send(202, "text/plain", "is not playing");
    }
  });

  server.on("/hand/right/vibration/play", HTTP_POST, [](){
    if(!isPlaying){
      isPlaying = true;
      server.send(202, "text/plain", "started playing");
    } else {
      server.send(202, "text/plain", "already playing");
    }
   
   
  });
  

  

  server.onNotFound(handleNotFound);

  server.begin();
  Serial.println("HTTP server started");
}

void loop(void) {
  server.handleClient();
  delay(2);//allow the cpu to switch to other tasks
}
