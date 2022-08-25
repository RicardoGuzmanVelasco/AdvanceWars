### Refactor

- No exponer unit en batallion.
- public space at
- No debería de haber set en el terreno.
- Duplicación espacios/bounds.
- Hay un problema con los nullobjectpatterns.

- La relación entre allegiances es o aliado o neutral o enemigo. Enum viable.

  - No puede/debe instanciarse más de un objecto del mismo NullOjectPattern.
    - Esto lo explica bien Martin Fowler en el PoEAA en el capítulo Special Case.
    - Eso se resuelve quitando operador => y poniendo {get;} =.
      - Aun así está fallando la igualdad a veces.
      - Ejemplo: BattalionNull vs BattalionNull.
  - También podríamos usar el INull que dice Fowler para resolver los == con is INull y tal.
- Hay que hacer builders aún de cosas como TheatreOps.
- Redondear 0.05 los outcomes de ataques y tal.
- Posible composición: teatro de operaciones se compone de espacio.
  - Esto permite que el reporte de bajas se haga sobre el teatro de operaciones.
    - Se gana en semántica y en acercamiento al dominio.
    - Se pierde en diseño (porque se vulnera ley de Demeter, experto en información, envidia de características, blablablá).

### WIP

- Ahora mismo está mockeado el caso de blocker.
  - Falta un algoritmo de camino mínimo y demás.
- Está sin implementar qué tácticas puede hacer un batallón según su:
  - Unidad.
  - Posición en el mapa.
  - Etc.
- Una Unit tiene que pertenecer a  una armada (naval, aérea, terrestre).
  - Esto permite cosas como que ciertos edificios solo curen ciertas unidades.
- Está ahora mismo mockeado todo lo que tiene que ver con el Player. Por eso tiene un Id. 
  - No tiene sentido realmente que tenga un Id.
    - Lo pusimos para que fuera fácil depurar comparándolo con la nación.
    - No hay que olvidar que un Player es simplemente un patrón estrategia de control de juego.
      - Player humano, player de la máquina que puede ser más o menos agresivo, tonto, etc.
  - Esto nos lleva a que ahora mismo hay una duplicidad muy rara y tonta en los test porque coincide nación con id y además ya coincide de por sí nación del batallón con la del player y blablablá.
    - Esa API se puede mejorar.

### Riesgos

* Si atacas y te matan en el contrataque, puede ser problemático (o no) que se llame a una maniobra de Wait sobre ti (esa maniobra se llama automáticamente tras el Fire).

### Features para hacer

* Niebla de guerra.
  * Visibilidad de casillas por terreno.
  * Concepto de visibilidad en las unidades.
  * Emboscadas.
* Recuperación de vida y ammo en edificios.
  * Edificios curan 20.
* Barracas y generalización.
* Sistema de economía.
  * Ganar dinero por edificios.
  * Gastar dinero en spawnear batallones.
* Combustible/munición.
* Rango de ataque.
* Ataques de artillería.
* Seleccionar a qué enemigo atacar.
