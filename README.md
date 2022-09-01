# HMI
Aplicacion creada en Windows Forms que permite obtener los datos de un sensor LM35, un potenciometro y un sensor ultrasonico y permite presentarlos en una HMI
hecha en windowsForms con C#,desde una placa arduino mega, posteriormente envia los datos a un servidor azure, y los publica en un Web Service realizado en MVC
CORE del mismo azure, ademas cuneta con un apk paraandroid, realizada en xamarin forms la cual permite visualizar los datos de manera remota, cuando el sensor
LM35 alcanza los 25 grados, prende un servomotor simulando refrigeracion, al mismo tiempo envia una notificacion al apk, alertando del incremento de temperatura
y del encendido de la refrigeracion, cuenta tambien con graficas para visualizar los cambios en lso sensores.

INTERFAZ APK

[![interfaz.png](https://i.postimg.cc/L8t7ryhq/interfaz.png)](https://postimg.cc/gx0NhHVd)

*Interfaz de visualizacion

[![notificaciones.png](https://i.postimg.cc/pdGt601f/notificaciones.png)](https://postimg.cc/06w36Cvj)

*Notificaciones de Temperatura
