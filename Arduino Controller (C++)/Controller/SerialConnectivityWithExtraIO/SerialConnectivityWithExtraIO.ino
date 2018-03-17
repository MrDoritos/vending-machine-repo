#include <Adafruit_MotorShield.h>

char incoming[2];
char digital[4];
//For MotorShield
const int stepsPerRev = 200;
const int primaryStepperSteps = 360;
int secondaryStepperSteps;

Adafruit_MotorShield AFMS = Adafruit_MotorShield();

Adafruit_StepperMotor *primaryStepper = AFMS.getStepper(200, 1);
Adafruit_StepperMotor *secondaryStepper = AFMS.getStepper(200, 2);
//End For MotorShield
void setup() {
  // put your setup code here, to run once:
  //Serial.setTimeout(100);
  Serial.begin(9600);
  for (int i = 2; i <= 10; i++) {
    pinMode(i, INPUT_PULLUP);
  }
  for (int i = 11; i <= 13; i++) {
    pinMode(i, OUTPUT);
  }

  //For MotorShield
  AFMS.begin();
  primaryStepper->setSpeed(50);
  secondaryStepper->setSpeed(50);
  //End For MotorShield
}

void loop() {
  // put your main code here, to run repeatedly:
  for (int i = 4; i <= 7; i++) {
    digital[i - 3] = !(digitalRead(i));
    //Serial.print(int(digital[i - 3]));
    //Serial.print("\n");
    //delay(100);
  }
  if (bool(digital[1])) {
    Serial.print("C0");
    delay(500);
  }
  if (bool(digital[2])) {
    Serial.print("A0");
    delay(500);
  }
  if (bool(digital[3])) {
    Serial.print("B3");
    delay(500);
  }
  if (bool(digital[4])) {
    Serial.print("D0");
    delay(500);
  }
  
  if (Serial.available()>1) {
    //incoming[] = { Serial.read(), Serial.read() };
    //incoming[1] = Serial.read();
    //incoming[2] = Serial.read();
    for (int i = 1; i <= 2; i++) {
      incoming[i] = Serial.read();
      //Serial.print(i);
      //Serial.print("\n");
    }
    switch (incoming[1]) {
      case 'A':
      //Case For "Move Command"
      //Serial.print("option a");
      motorFunction((incoming[2] - '0')*45);
      break;
      case 'B':
      //Case For "One's Place Balance"
      //Serial.print("option b");
      break;
      case 'C':
      //Case For "Tenth's Place Balance"
      break;
      case 'D':
      //Case For "Hundreth's Place Balance"
      break;
      case 'E':
      BlinkLight();
      //Case For Exception "Nothing Selected"
      //Serial.print("nosel ");
      break;
      case 'F':
      BlinkLight();
      //Case For Exception "Not Enough Balance"
      //Serial.print("nobal ");
      break;
      case 'G':
      BlinkLight();
      //Case For Exception "Out Of Stock"
      //Serial.print("outofstock");
      break;
      case 'I':
      giveChange(incoming[2]);
      break;
      default:
      loop();
      break;
    }
  }
  delay(20);
}

int motorFunction(int secondaryStepperSteps) {
  //Serial.print(secondaryStepperSteps);
  //Serial.print("\n");
  //Serial.print(secondaryStepperSteps);
  //Serial.print((secondaryStepperSteps/360.0)*200.0);
  
  secondaryStepper->step(((secondaryStepperSteps/360.0)*200.0), FORWARD, DOUBLE);

  primaryStepper->step(((primaryStepperSteps/360.0)*200.0), FORWARD, DOUBLE);

  secondaryStepper->step(((secondaryStepperSteps/360.0)*200.0), BACKWARD, DOUBLE);
  //Call loop, do not return
  loop();
}

int giveChange(int WhichCoin) {
  
}

void BlinkLight() {
  digitalWrite(12, HIGH);
  delay(1000);
  digitalWrite(12, LOW);
}

