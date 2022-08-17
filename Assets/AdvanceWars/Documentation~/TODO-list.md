### Refactor
- No exponer unit en batallion.
- Eliminar el problema de tener que estar comprobando si existe el espacio en el mapa.
    - O bien iniciar los espacios, o bien encapsular en una estructura para que se inicien by default bajo consulta.
- public space at
- No debería de haber set en el terreno.
- Duplicación espacios/bounds.

### Feature
- Ahora mismo está mockeado el caso de blocker.
  - Falta un algoritmo de camino mínimo y demás.