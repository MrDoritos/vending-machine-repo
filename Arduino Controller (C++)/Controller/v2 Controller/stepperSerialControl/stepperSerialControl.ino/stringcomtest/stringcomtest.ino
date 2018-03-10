#include <Adafruit_MotorShield.h>

int incoming = 0;
int stepsPerRev = 200;
int primaryStepperSteps = 360;
int secondaryStepperSteps;
long counter;

Adafruit_MotorShield AFMS = Adafruit_MotorShield();

Adafruit_StepperMotor *primaryStepper = AFMS.getStepper(200, 1);
Adafruit_StepperMotor *secondaryStepper = AFMS.getStepper(200, 2);




void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(8, INPUT_PULLUP);
  pinMode(9, INPUT_PULLUP);
  pinMode(7, INPUT_PULLUP);
  pinMode(6, INPUT_PULLUP);
  Serial.setTimeout(50);
  
  AFMS.begin();
  primaryStepper->setSpeed(50);
  secondaryStepper->setSpeed(50);
}

void loop() {
incoming = 0;
  // put your main code here, to run repeatedly:
  //incoming = Serial.readString();
  delay(100);
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
    char in = Serial.read();
    if (in != "") {
    Serial.print(in + '\n');
    incoming = int(in);
    switch (incoming){
      default:
      break;
    }
    
    
    
    }
  }
  //Serial.println(Serial.readString());
  //if (incoming != ""){
  //Serial.println(incoming);  
  //}
//}


int motorFunction(int secondaryStepperSteps) {
  //Serial.print("Moving to.. '" + "n" + "', " + "at a " + secondaryStepperSteps + " degree angle\n");
  //Bluetooth.println("Moving to.. '" + in + "', " + "at a " + secondaryStepperSteps + " degree angle");
  secondaryStepper->step(((secondaryStepperSteps/360.0)*200.0), FORWARD, DOUBLE);
  //delay(100);
  //primaryStepper->step(((primaryStepperSteps/360.0)*200.0), FORWARD, DOUBLE);
  //delay(100);
  //secondaryStepper->step(((secondaryStepperSteps/360.0)*200.0), BACKWARD, DOUBLE);
  //Serial.print("Finished operation\n");
  //Bluetooth.println("Finished, waiting 100ms");
  delay(100);
  loop();
}



