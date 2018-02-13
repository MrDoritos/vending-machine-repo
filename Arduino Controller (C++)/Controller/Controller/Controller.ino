#include <Stepper.h>

String incoming = "";

Stepper SelectorStepper(200, 2, 3, 4, 5);
Stepper PrimaryStepper(200, 6, 7, 8, 9);

void setup() {
    Serial.begin(9600);
    for (int i = 2; i < 6; i++){
      pinMode(i, OUTPUT);
    }
    SelectorStepper.setSpeed(60);
    PrimaryStepper.setSpeed(60);
    Serial.println("Scatman");
}

void loop() {
  if (Serial.available() > 0) {
    incoming = (Serial.readString());
    int s = 0;
    s = String(incoming);
    SelectorStepper.step(((200)/365)*s);
    PrimaryStepper.step(200);
    SelectorStepper.step(-(((200)/365)*s));
    String artused = String((200*s)/360));
    Serial.println("Worked");
    Serial.println(artused);
  }
}
