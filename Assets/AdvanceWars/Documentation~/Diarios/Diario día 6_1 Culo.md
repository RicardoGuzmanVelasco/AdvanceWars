# Día 6.1 

## Culo

Duración: 

### Intenciones

- Iré añadiendo aquí cosas que voy haciendo.

- [X] Ahora no se pueden setear forces fuera del máximo y del mínimo.
- [ ] Refactor maniobras que llaman a maniobras
- [X] Movida responsabilidad de heal a batallon.
- [X] Fix los edificios curaban unidades enemigas.
    - [X] Movida responsabilidad de curarse al batallón al clampeo
- [X] Ganar dinero por edificios.
- [X] Spawnear un batallon gasta dinero.
- [X] Merge de batallones da WarFunds
- [X] Refactor. No pasar por parametro el treasury
- [X] La situacion tiene que tener una allegiance.
- [ ] Mover la responsabilidad de la cura de la situacion a los terrains y spaces.
- [ ] Test se cura cuando se llama al final del turno. (Haria falta? No se si aporta mucho teniendo en cuenta que sabemos que cuando se acaba un turno se hace begin en el commanding officer, por las maniobras)
- [ ] Reparar cuesta dinero.

### Conclusiones

- Si he añadido precondiciones sigue siendo refactor? Entiendo que no.
- Es rarisimo lo de las maniobras que llaman a maniobras. Necesito ayuda
- Es raro tener que pasar el treasury a la maniobra de reclutamiento. Habrá que pasar algo más que el mapa al apply. Situación?