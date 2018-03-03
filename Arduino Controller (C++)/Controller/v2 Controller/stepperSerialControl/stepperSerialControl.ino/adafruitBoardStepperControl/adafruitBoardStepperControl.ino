#include <Adafruit_MotorShield.h>
//#include <SoftwareSerial.h>

//#include <Stepper.h>
int stepsPerRev = 200;
int primaryStepperSteps = 360;
int secondaryStepperSteps;
String in;
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
  AFMS.begin();
  //Bluetooth.begin(9600);
  primaryStepper->setSpeed(50);
  secondaryStepper->setSpeed(50);
}

void loop() {
  // put your main code here, to run repeatedly:
  while (Serial.available()) {
    char tmp = Serial.read();
  //while (Bluetooth.available()) {
    //char tmp = Bluetooth.read();
    delay(50);
    in = String(tmp);
    Serial.println("Operating with.. '" + in + "'");
    //Bluetooth.println("Operating with.. '" + in + "'");
    switch (tmp) {
     case 'A':
      motorFunction(45);
      break;
     case 'B':
      motorFunction(90);
      break;
     case 'C':
      motorFunction(135);
      break;
     case 'D':
      motorFunction(180);
      break;
     case 'E':
      motorFunction(225);
      break;     
     case 'Z':
      motorFunction(360);
      break;
      default:
      Serial.println("Unknown operation.. '" + in + "'");
      //Bluetooth.println("Unknown operation.. '" + in + "'");
      loop();
      break; 
    }   
  }
  delay(100);
}

int motorFunction(int secondaryStepperSteps) {
  Serial.println("Moving to.. '" + in + "', " + "at a " + secondaryStepperSteps + " degree angle");
  //Bluetooth.println("Moving to.. '" + in + "', " + "at a " + secondaryStepperSteps + " degree angle");
  secondaryStepper->step(((secondaryStepperSteps/360.0)*200.0), FORWARD, DOUBLE);
  //delay(100);
  //primaryStepper->step(((primaryStepperSteps/360.0)*200.0), FORWARD, DOUBLE);
  //delay(100);
  //secondaryStepper->step(((secondaryStepperSteps/360.0)*200.0), BACKWARD, DOUBLE);
  Serial.println("Finished operation");
  //Bluetooth.println("Finished, waiting 100ms");
  delay(10);
  loop();
}

