# vending-machine-repo
Programs and sources for both the Arduino and .NET-capable hosts for an obscure vending machine.

[Communication between Arduino and Host]
  Communication will be done using UART. The host will use a one byte ASCII character to instruct the Arduino to the correct, preprogrammed
'column'. All Serial COMs should be on COM3, 9600 baud, 1 stop bit in 8 bit words.

[Arduino]
  The Arduino uses a switch-case statement to control the steppers. You can call the function with "motorFunction()", with an integer
argument for the degree of rotation. The function always turns the primary stepper a full rotation. motorFunction always returns to the
main loop.

[Host]
  Programmed with C# on the .NET v4.6 Framework, a port to Linux using mono is to be determined. Make sure the COM3 port is changed to its
(Hopefully static) COM-equivalent port. The program is strictly only an interface for the Arduino at this time, nothing more.

[Interface]
  Nothing much, but there are plans.
