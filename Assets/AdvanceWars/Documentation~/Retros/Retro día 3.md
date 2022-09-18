# Dia 3

## Retro

### Ricardo

- Solo mencionar que correlacionar cobertura alta y seguridad es peligroso, aunque ya lo sepamos.

- Algunos test se quedaban inconclusos en Rider, he tenido que tirarlos en Unity.
  - ¿A alguien más le ha pasado?
  - Se soluciona reiniciando Rider. No me aparecían vuestros nuevos test siquiera.

- He sacado de aquí el vocabulario para fuerzas armadas. Hay mucha ambigüedad si se piensa en castellano.
  - https://en.wikipedia.org/wiki/Military

- ¿Por qué hay una carpeta Tests dentro de la carpeta Builders que está dentro de la carpeta Tests?

- Propuesta como práctica en este pet project: pasar a nullable reference.
  - http://www.neuston.io/nullable-reference-types-in-unity/

- No me terminaba de convencer la estructura de los test que en GameTests espían que se lance el evento del cursor. 
  - Me estaba amoldando a esa forma pero he preferido seguir otra para tenerla como propuesta alternativa a ver qué parecía.

## Diseño

### Alejandro

- ¿Terrain no debería ser un value object?

### Ricardo

- No entiendo el propósito de la clase RangeOfBuilder.
  - A colación con esto, se ha quedado sin usos el builder de mapa.
- Podríamos hablar del terreno Air, que se crea como factory method y todo el tema de meterle el Id.
- Comentaba Alejandro que se le hace raro pasar terreno a theaterops y que luego coja otro.
  - A mí me parece de lo más normal. Tú le pasas el contexto de partida y él se preocupa de que sea o no ese.

- He añadido valores por defecto a los setters de Unit porque si no se podían quedar cosas a nulo.
  - Habría que ver si esos valores son correctos o queremos otros. He usado los que mejor he podido.
  - Esto deja el flyweight de Unit como el constructor por defecto, básicamente.

- Ahora mismo el WhereIs/CursorCoord se me queda solo usado por un test y lo iba a quitar, pero:
  1. No sabría cómo probar lo que prueba ese test (imagino que el test desaparece).
  2. Creo que va a ser usado cuando empiece el flujo de control de maniobras y tal.
  - Así que lo he dejao.

## Conclusiones generales

- El que las clases de dominio gestionen datos no es problema ya que hay un caso especial que es el terreno aire. Como está ahora está guay.
  - Si en un futuro pasa algo con los submarinos, puede ser interesante que las unidades conozcan el terreno en el que están.
- Que se ponga refactor o test cuando se haga un refactor de test, es a discrección de cada persona.
- Hacer un test comprobando un efecto secundario, no está mal para no exponer datos que no queremos exponer, como con lo del cursor.
  - Se comentó una charla que hablaba de tests de resultado, estado e interacción (o algo así, revisar)
- En el tema de que se había complicado tanto el movimiento del mapa, sabiendo que son grafos, podemos probar a hacer los grafos con TDD y luego usarlos para hacer el refactor.
- Los no Value Objects no deben de tener un Flyweight en un factory method, el estado les va a acabar afectando y petará por mil sitios.
- Las nullable references Ricardo las va a probar en otro proyecto.
- En muchos casos se estaba haciendo el act más grande de lo que debería, cuando eso acaba causando problemas a nivel de entendimiento.
- Hacer inicializadores por defecto a los nullobject para que no se queden referencias a null.
- El builder de RangeOfMovement no termina de convencer pero no se ha decidido nada sobre ello