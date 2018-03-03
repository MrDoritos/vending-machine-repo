String incoming = "";

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  pinMode(8, INPUT_PULLUP);
  pinMode(9, INPUT_PULLUP);
  pinMode(7, INPUT_PULLUP);
  pinMode(6, INPUT_PULLUP);
  Serial.setTimeout(50);
}

void loop() {
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
  if ((Serial.available())!=1) {
    incoming = Serial.readString();
    if (incoming != "") {
    Serial.print(incoming + '\n');
    }
  }
    
  //Serial.println(Serial.readString());
  //if (incoming != ""){
  //Serial.println(incoming);  
  //}
}
