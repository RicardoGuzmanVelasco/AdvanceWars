# Día 5

# Alejandro

Duración: ~2.5h 

### Intenciones

- [x] FireTactic consume Ammo.
- [ ] Recuperación de Ammo en Buildings. 

### Conclusiones

- Un poco de lío con donde dejar los tests sobre FireManeuver: ¿en FireTests? ¿o en OrderTests? 
- No me convence lo de space.HealOccupant cuando se puede hacer space.Occupant.Heal. ¿Demeter?
- He ido a añadir precondiciones a `space.HealOccupant()` y `space.ReplenishOccupantAmmo()`, y fallaban los tests por ello. Me ha dado pereza sacar el BuildingBuilder para poder asignar fácilmente una nación, pero me ha dado todavía más pereza adaptar los tests. Lo dejo pendiente para futuras sesiones.
- Faltarían caminos tristes de _"Recuperación de Ammo en Buildings"_.

## Culo

Duración: xH

### Intenciones

- [X] Un edificio tipo barracón spawnea batallones.
- [X] Un barracón solo puede spawnear batallones de unidades de una armada en concreto.
- [ ] Los batallones spawneados no tienen maniobras disponibles.

### Conclusiones

- Me siento muy lento hoy y muy tonto, me cuesta hacer cosas básicas. Imagino que vendré frito de la jam y de hacer código basura.
