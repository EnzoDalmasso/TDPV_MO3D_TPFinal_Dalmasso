# TP Final MO3D

Trabajo práctico final que hice en Unity 2022.3 (febrero y marzo de 2024). Es un juego de acción en un templo donde el jugador pelea con una espada contra zombis: cuando mata a todos los del nivel pasa al siguiente, y si se queda sin vida aparece la pantalla de Game Over.

Tiene un menú inicial y dos niveles. Los scripts de `Assets/Scrips/` los escribí yo en C#; el escenario (Sun Temple), el modelo del personaje y los zombis son assets externos.

## Cómo abrirlo

1. Abrir la carpeta del repo desde Unity Hub con la versión **2022.3.21f1**.
2. Abrir la escena `Assets/Scenes/MenuInicial.unity` y darle Play.

| Tecla | Acción |
|---|---|
| WASD | moverse |
| Mouse | mover la cámara |
| Espacio | saltar |
| Click izquierdo | atacar con la espada |
| P | pausa |

## Cómo está hecho

Los scripts están en `Assets/Scrips/`.

`GeneradorZombi.cs` instancia la cantidad de zombis que se le indique en el Inspector, en posiciones al azar dentro de un área. A cada uno le engancha un callback al `Action morir` del zombi, entonces cada vez que muere uno se descuenta del total; cuando llega a cero dispara su propio `Action victoria`. `Menues.cs` está suscripto a ese evento y muestra la pantalla de siguiente nivel. De esta forma el generador no conoce nada de la UI y el zombi no conoce nada del generador.

El comportamiento del zombi (`Enemigo.cs`) depende de la distancia al jugador:

- A más de 5 unidades deambula. Cada 4 segundos elige al azar entre quedarse quieto o girar hacia un ángulo aleatorio y caminar.
- Entre 5 y 1 unidad gira hacia el jugador y lo persigue corriendo.
- A menos de 1 ataca. El fin del ataque lo avisa un Animation Event (`Final_Ani`), que resetea el tiempo entre golpes.

Cuando muere se activa la animación correspondiente y el cuerpo se destruye a los 5 segundos.

En el jugador (`Player.cs`) el salto usa un `Rigidbody` con `AddForce` y solo se permite si un trigger en los pies (`LogicaPies.cs`) está tocando el suelo. Mientras ataca no se puede mover. El daño entra por un trigger con el tag `manos` de los zombis y la vida se muestra con una `Image` en modo fill. `MenuPausa.cs` congela el juego con `Time.timeScale = 0` y libera el cursor.

## Lo que cambiaría hoy

Los zombis se mueven con `transform.Translate`, así que no esquivan paredes; con un `NavMeshAgent` eso se resuelve casi gratis. El jugador se busca con `GameObject.Find("Player")` en cada zombi, y la barra de vida divide por un número fijo (14) en vez de usar la vida máxima. También pasaría todo al Input System nuevo y haría que el botón Salir cierre el juego de verdad (ahora solo deja un log).
