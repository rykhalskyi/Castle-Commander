import { Sector } from './sector';

export interface Hexagon {
  index : number;

  color : string;
  sectors : Sector[];

  defaultSectorAttack : number;
  defaultSectorDefence : number;
}
