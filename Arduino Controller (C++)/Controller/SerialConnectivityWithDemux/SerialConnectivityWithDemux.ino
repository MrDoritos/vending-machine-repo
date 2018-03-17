#include <Adafruit_MotorShield.h>

char incoming[2];
char digital[4];

int num_array[10][7] = {  { 1,1,1,1,1,1,0 },    // 0
                          { 0,1,1,0,0,0,0 },    // 1
                          { 1,1,0,1,1,0,1 },    // 2
                          { 1,1,1,1,0,0,1 },    // 3
                          { 0,1,1,0,0,1,1 },    // 4
                          { 1,0,1,1,0,1,1 },    // 5
                          { 1,0,1,1,1,1,1 },    // 6
                          { 1,1,1,0,0,0,0 },    // 7
                          { 1,1,1,1,1,1,1 },    // 8
                          { 1,1,1,0,0,1,1 }};   // 9
                          
//For MotorShield
const int stepsPerRev = 200;
const int primaryStepperSteps = 360;
int secondaryStepperSteps;

Adafruit_MotorShield AFMS = Adafruit_MotorShield();

Adafruit_StepperMotor *primaryStepper = AFMS.getStepper(200, 1);
Adafruit_StepperMotor *secondaryStepper = AFMS.getStepper(200, 2);
//End For MotorShield

void setup() {
  Serial.begin(9600);
  
  for (int i = 2; i <= 7; i++) {
    pinMode(i, OUTPUT);
  }
  
  pinMode(8, INPUT_PULLUP);

  //For MotorShield
  AFMS.begin();
  primaryStepper->setSpeed(50);
  secondaryStepper->setSpeed(50);
  //End For MotorShield
}

void loop() {
  delay(250);
  //Serial.println("Returned to Loop");
  for (int i = 35; i < 49; i++) {
    //Mux for address i
    int a = i %2; //LSB
    int b = i/2 %2;
    int c = i/4 %2;
    int d = i/8 %2;
    int e = i/16 %2;
    int f = i/32 %2; //MSB
    digitalWrite(2, f); //MSB
    digitalWrite(3, e);    
    digitalWrite(4, d);
    digitalWrite(5, c);
    digitalWrite(6, b);
    digitalWrite(7, a); //LSB
    Serial.println();
    Serial.print(f);
    Serial.print(e);
    Serial.print(d);
    Serial.print(c);
    Serial.print(b);
    Serial.print(a);
    Serial.print(" ");
    Serial.print(i);
    Serial.print(" ");

    //Addresses 0 - 34 ARE FOR WRITING! Do not read from these addresses, EVER!
    //Addresses 35 - 48 ARE FOR READING! Do not write to these addresses, nothing will happen!
    
    //Read data at address i
    bool stat = digitalRead(8);
    Serial.print(stat);
    Serial.print(" ");
    if (stat) {
      delay(250);
      switch (i) {
        //ENTER
        case 35:
        Serial.print("C0");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //SELECT A0
        case 36:
        Serial.print("A0");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //SELECT A1
        case 37:
        Serial.print("A1");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //SELECT A2
        case 38:
        Serial.print("A2");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //SELECT A3
        case 39:
        Serial.print("A3");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //SELECT A4
        case 40:
        Serial.print("A4");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //SELECT A5
        case 41:
        Serial.print("A5");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //SELECT A6
        case 42:
        Serial.print("A6");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //SELECT A7
        case 43:
        Serial.print("A7");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //REQUEST CHANGE
        case 44:
        Serial.print("D0");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //1 CENT IN
        case 45:
        Serial.print("B0");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //5 CENTS IN
        case 46:
        Serial.print("B1");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //10 CENTS IN
        case 47:
        Serial.print("B2");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
        //25 CENTS IN
        case 48:
        Serial.print("B3");
        //digitalWrite(9, HIGH); //Place this code in when you make so you can reset addresses
        //digitalWrite(9, LOW); //Place this code in when you make so you can reset addresses
        break;
      }
    }
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
      SevenSegmentSet(0, (incoming[2] - '0'));
      break;
      case 'C':
      //Case For "Tenth's Place Balance"
      SevenSegmentSet(1, (incoming[2] - '0'));
      break;
      case 'D':
      //Case For "Hundreth's Place Balance"
      SevenSegmentSet(2, (incoming[2] - '0'));
      break;
      case 'E':
      BlinkLight(10);
      //Case For Exception "Nothing Selected"
      //Serial.print("nosel ");
      break;
      case 'F':
      BlinkLight(11);
      //Case For Exception "Not Enough Balance"
      //Serial.print("nobal ");
      break;
      case 'G':
      BlinkLight(12);
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
  Serial.println("WhichCoin Method Called");
  if (WhichCoin < 4) {
    WhichCoin = WhichCoin + 28;
    int a = WhichCoin %2; //LSB
    int b = WhichCoin/2 %2;
    int c = WhichCoin/4 %2;
    int d = WhichCoin/8 %2;
    int e = WhichCoin/16 %2;
    int f = WhichCoin/32 %2; //MSB
    digitalWrite(2, f); //MSB
    digitalWrite(3, e);    
    digitalWrite(4, d);
    digitalWrite(5, c);
    digitalWrite(6, b);
    digitalWrite(7, a); //LSB
    //digitalWrite(9, HIGH); //Add when able to give change
    //delay(1000); //Add when able to give change
    //digitalWrite(9, LOW); //Add when able to give change
  }
}

int BlinkLight(int Pin) {
  Serial.println("BlinkLight Method Called");
  digitalWrite(Pin, HIGH);
  delay(1000);
  digitalWrite(Pin, LOW);
}

int SevenSegmentSet(int Display, int Value) {
  Serial.println("SevenSegmentSet Method Called");
  int selDisplay = Display * 7;
  int o = 0;
  for (int i = selDisplay; i < (selDisplay + 7); i++) {
    o++;
    int a = i %2; //LSB
    int b = i/2 %2;
    int c = i/4 %2;
    int d = i/8 %2;
    int e = i/16 %2;
    int f = i/32 %2; //MSB
    digitalWrite(2, f); //LSB
    digitalWrite(3, e);
    digitalWrite(4, d);
    digitalWrite(5, c);
    digitalWrite(6, b);
    digitalWrite(7, a); //MSB
    Serial.print(num_array[Value][o - 1]);
    digitalWrite(9, num_array[Value][o - 1]);
  }
  Serial.println();
    //Serial.print(num_array[Value + 1][1]);
}

