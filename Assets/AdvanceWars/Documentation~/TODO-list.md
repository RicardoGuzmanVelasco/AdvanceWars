### Refactor

- Reusar cosa de coordenadas/grafos/etc. los rangos de fuego y de movimiento.
- Renombrar RangeOfFire para que no se llame igual la clase, la property y el metodo del mapa.
- Tenemos 2 metodos Range of movement, uno le pasamos un batallon y busca el espacio y llama al otro. El otro coge el espacio y pilla el batallon. Esto se hace por tests.
- MoveCostOf en Space.
- Eliminar el Invitado en el contexto del Merge.
- Maniobras que llaman a otras maniobras (en vez de llamadas con casteos desde el commanding officer)
- Los buildings crean un Building como siege result. Ha hecho falta duplicar los constructores para que eso no de problemas
- Las maniobras automaticas de cura, recuperacion... No son maniobras ahora mismo.
- Crear clase de tipo de building y usarla como modelo.

### Docs

- Añadir maneuvers existentes al diagrama del modelo de dominio.
- Separar diagrama del modelo de dominio en varios para mejorar legibilidad.

### WIP

- [X] Sacar abstracción {número máximo, número actual}... ¿reutilizar gauge (con suelo cero)?
  - [X] Es para el MaxSiegePoints y el SiegePoints. Es un patrón muy muy repetido y no es responsabilidad del buildin
  - Tambien la vida, la municion...
  - Faltaría revisar lo del suelo cero también + munición + germinación del .Value
    - La Munición de Armas todavía no tiene el concepto de Munición máxima. 
- Ahora mismo el itinerario de la maniobra de movimiento no está probado. Cuando se haga la niebla de guerra hace falta.
- Ahora mismo está mockeado el caso de blocker.
  - Falta un algoritmo de camino mínimo y demás.
- Está ahora mismo mockeado todo lo que tiene que ver con el Player. Por eso tiene un Id.
  - No tiene sentido realmente que tenga un Id.
    - Lo pusimos para que fuera fácil depurar comparándolo con la nación.
    - No hay que olvidar que un Player es simplemente un patrón estrategia de control de juego.
      - Player humano, player de la máquina que puede ser más o menos agresivo, tonto, etc.
  - Esto nos lleva a que ahora mismo hay una duplicidad muy rara y tonta en los test porque coincide nación con id y además ya coincide de por sí nación del batallón con la del player y blablablá.
    - Esa API se puede mejorar.
- ¿Desde dónde controlar que el cursor ya está en un borde y no tiene que ir más para allá?
- Recuperación de Ammo en Buildings.
  - Falta comprobar (y seguramente soportar) los caminos tristes.

### Riesgos

- Si atacas y te matan en el contrataque, puede ser problemático (o no) que se llame a una maniobra de Wait sobre ti (esa maniobra se llama automáticamente tras el Fire).
  - Por ejemplo por guardar maniobras ejecutadas sobre un Battalion.Null...
- Ahora mismo no tenemos separacion entre Aliado y propio. Cuando hagamos el 2vs2, muchas cosas en las que se comprueba si es Ally, petaran, ya que deberian de ser ally las unidades del compañero.
  - Habrá que añadir un método Self.
  - También hemos hablado aquí de lo de la abstracción National que segrega de Allegiance.
- Quizá habría que hacer condiciones de disponibilidad de uso de las tácticas.
  - Se ha discutido pero no se ha llegado a nada en claro.
- Las entidades (batallón, terrain/building...) corren el peligro de que sus clientes se queden con referencias desactualizadas, ya que se sustituyen por otras por ejemplo cuando termina un asedio
- ¿Qué ocurre si se le pregunta IsAlly a un INull?.
- ¿Qué pasa si se hace put del mismo batallón en dos coordenadas distintas del mapa?
- Al spawnear un batallon le da inmediatamente la orden de wait. Eso puede crear problemas cuando se usen las maniobras para ir adelante y atrás y tal.
### Tests

### Features para hacer

- Redondear 0.05 los outcomes de ataques y tal.
- Los battalions al ser mergeados, devuelven el porcentaje sobrante de fuerzas en fondos.
- Niebla de guerra.
  - Visibilidad de casillas por terreno.
  - Concepto de visibilidad en las unidades.
  - Emboscadas.
- Unidades de una cierta Armada solo pueden ser curados en Edificios de dicha Armada.
- Combustible.
- Ataques de artillería.
  - Contrataque de artillería. (la artillería no contraataca / a la artillería nunca le contraatacan)
- Seleccionar a qué enemigo atacar.
- Táctica de sumergirse.
- Unidades de transporte.
- Condiciones de derrota.
  - Pillar headquarters, derrotan a todos tus batallones.
- Condiciones de victoria.
- Bandos para 2 vs 2.
- Commanding Officers con habilidades/pasivas (únicas por cada uno).
- Undo de maniobras cuando todavia no se ha hecho wait de la unit.
- Maniobras automáticas al inicio del turno de un CO.
- Soportar conceptos de Arma Principal y Secundaria.
  - La Principal tiene Munición máxima.
  - La Secundaria tiene Munición ilimitada.
