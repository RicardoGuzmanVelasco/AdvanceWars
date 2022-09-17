# Dia 3

## Retro

### Ricardo

- Solo mencionar que correlacionar cobertura alta y seguridad es peligroso, aunque ya lo sepamos.

- Algunos test se quedaban inconclusos en Rider, he tenido que tirarlos en Unity.
  - ¿A alguien más le ha pasado?
  - Se soluciona reiniciando Rider. No me aparecían vuestros nuevos test siquiera.

- He sacado de aquí el vocabulario para fuerzas armadas. Hay mucha ambigüedad si se piensa en castellano.
  - https://en.wikipedia.org/wiki/Military

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