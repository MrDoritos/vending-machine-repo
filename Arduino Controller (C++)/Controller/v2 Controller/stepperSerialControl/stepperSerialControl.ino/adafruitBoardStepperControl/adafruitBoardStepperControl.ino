#include <Adafruit_MotorShield.h>
//#include <SoftwareSerial.h>

//#include <Stepper.h>
int stepsPerRev = 200;
int primaryStepperSteps = 360;
int secondaryStepperSteps;
String in;
String tmp = "";
//SoftwareSerial Bluetooth(2, 3);
Adafruit_MotorShield AFMS = Adafruit_MotorShield();
//Adafruit_MotorShield AFMS = Adafruit_MotorShield();
Adafruit_StepperMotor *primaryStepper = AFMS.getStepper(200, 1);
Adafruit_StepperMotor *secondaryStepper = AFMS.getStepper(200, 2);
//Stepper primaryStepper(stepsPerRev, 2, 3, 4, 5);
//Stepper secondaryStepper(stepsPerRev, 6, 7, 8, 9);

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(8, INPUT_PULLUP);
  pinMode(9, INPUT_PULLUP);
  pinMode(7, INPUT_PULLUP);
  pinMode(6, INPUT_PULLUP);
  Serial.setTimeout(50);
  AFMS.begin();
  //Bluetooth.begin(9600);
  primaryStepper->setSpeed(50);
  secondaryStepper->setSpeed(50);
}

void loop() {
  // put your main code here, to run repeatedly:

  
  while (Serial.available()) {  
    if ((Serial.available())!=1) {
    tmp = Serial.readString();
    if (tmp != "") {
    Serial.print(tmp + '\n');
    }
  }   
  //while (Bluetooth.available()) {
    //char tmp = Bluetooth.read();
    delay(50);
  bool stat = digitalRead(8);
  if (stat == 0) {
    Serial.print("BAL25\n");
    stat = 1;
  }
  bool enter = digitalRead(9);
  if (enter == 0) {
    Serial.print("ENTER\n");
    enter = 1;
  }
  bool selection = digitalRead(7);
  if (selection == 0) {
    Serial.print("REQMOVA\n");
    selection = 1;
  }
  bool selectiontwo = digitalRead(6);
  if (selectiontwo == 0) {
    Serial.print("REQMOVB\n");
    selectiontwo = 1;
  }
    in = String(tmp);
    Serial.print("Operating with.. '" + in + "'\n");
    //Bluetooth.println("Operating with.. '" + in + "'");
if (tmp == "MOVA") {
  motorFunction(45);
}
tmp = "";
//    switch (tmp) {
//     case "A":
//      motorFunction(45);
//      break;
//     case "B":
//      motorFunction(90);
//      break;
//     case "C":
//      motorFunction(135);
//      break;
//     case "D":
//      motorFunction(180);
//      break;
//     case "E":
//      motorFunction(225);
//      break;     
//     case "Z":
//      motorFunction(360);
//      break;
//      default:
//      Serial.print("Unknown operation.. '" + in + "'\n");
      //Bluetooth.println("Unknown operation.. '" + in + "'");
//      loop();
//      break; 
//    }   
  }
  delay(100);
}

int motorFunction(int secondaryStepperSteps) {
  Serial.print("Moving to.. '" + in + "', " + "at a " + secondaryStepperSteps + " degree angle\n");
  //Bluetooth.println("Moving to.. '" + in + "', " + "at a " + secondaryStepperSteps + " degree angle");
  secondaryStepper->step(((secondaryStepperSteps/360.0)*200.0), FORWARD, DOUBLE);
  //delay(100);
  //primaryStepper->step(((primaryStepperSteps/360.0)*200.0), FORWARD, DOUBLE);
  //delay(100);
  //secondaryStepper->step(((secondaryStepperSteps/360.0)*200.0), BACKWARD, DOUBLE);
  Serial.print("Finished operation\n");
  //Bluetooth.println("Finished, waiting 100ms");
  delay(10);
  loop();
}

