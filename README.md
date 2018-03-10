# vending-machine-repo
Programs and sources for both the Arduino and .NET-capable hosts for an obscure vending machine.

[Communication between Arduino and Host]
  Communication will be done using UART. The host will use 2 byte words to 'tell' the Arduino to change the 1st, 2nd, and 3rd 7 segment display, what types of coins to give when giving change, what slot to focus on, "None Selected" error, "Insufficient balance" error, and "Out of Stock". The Arduino, also using 2 byte words, will 'tell' the host what slot was just selected, what coin was just added, enter was pressed, and change was requested. All Serial COMs should be on COM3, 9600 baud, 1 stop bit in 8 bit words.

[Arduino]
  The Arduino uses a switch-case statement to control the steppers. You can call the function with "motorFunction()", with an integer
argument for the degree of rotation, and the work is held by the Adafruit MotorShield. The function always turns the primary stepper a full rotation. motorFunction always returns to the main loop.

[Host]
  Programmed with C# on the .NET v4.6 Framework, a port to Linux using mono is to be determined. Make sure the COM3 port is changed to its
(Hopefully static) COM-equivalent port. The program is strictly only an interface for the Arduino at this time, nothing more.

[Interface]
  Nothing much, but there are plans.
