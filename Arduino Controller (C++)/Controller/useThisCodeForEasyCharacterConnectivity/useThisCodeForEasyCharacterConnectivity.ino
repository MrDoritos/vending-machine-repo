void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  while (Serial.available()) {
    char tmp = Serial.read();
    delay(50);
    String in = String(tmp);
    Serial.println("Operating with.. '" + in + "'");
    switch (tmp) {
     case 'a':
      break;
     case 'b':
      break;
     case 'c':
      break;
     case 'd':
      break;
     case 'e':
      break;
      default:
      Serial.println("Unknown operation.. '" + in + "'");
      loop();
      break; 
    }   
      Serial.println("Moving to.. '" + in + "'");
      motorFunction();
  }
  delay(100);
}

void motorFunction() {
  loop();
}

