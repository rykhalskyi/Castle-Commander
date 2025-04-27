import {Resource} from './resource';

export interface ArchType{
  name : string;
  resourcesSorted : Resource[];
  resorcesForBronze : Resource[];
  resourceForSilver : Resource[];
  resourceForGold : Resource[];
  isShadow : boolean;
}

export const Prince : ArchType = {
  name : 'Prince',
  resourcesSorted : [Resource.Yellow, Resource.Violet, Resource.Navy, Resource.Red, Resource.Orange, Resource.Green, Resource.Blue],
  resorcesForBronze : [Resource.Yellow, Resource.Violet, Resource.Navy],
  resourceForSilver : [Resource.Bronze, Resource.Red, Resource.Orange],
  resourceForGold : [Resource.Silver, Resource.Red, Resource.Orange, Resource.Green, Resource.Blue],
  isShadow : false
}

export const Trickster : ArchType = {
  name : 'Trickster',
  resourcesSorted : [Resource.Navy, Resource.Orange, Resource.Red, Resource.Yellow, Resource.Green, Resource.Blue, Resource.Violet],
  resorcesForBronze : [Resource.Navy, Resource.Orange, Resource.Red],
  resourceForSilver : [Resource.Bronze, Resource.Yellow, Resource.Green],
  resourceForGold : [Resource.Silver, Resource.Yellow, Resource.Green, Resource.Blue, Resource.Violet],
  isShadow : false
}

export const Warrior : ArchType = {
  name : 'Warrior',
  resourcesSorted : [Resource.Red, Resource.Violet, Resource.Yellow, Resource.Orange, Resource.Green, Resource.Blue, Resource.Navy],
  resorcesForBronze : [Resource.Red, Resource.Violet, Resource.Yellow],
  resourceForSilver : [Resource.Bronze, Resource.Orange, Resource.Green],
  resourceForGold : [Resource.Silver, Resource.Orange, Resource.Green, Resource.Blue, Resource.Navy],
  isShadow : false
}

export const Landlord : ArchType = {
  name : 'Landlord',
  resourcesSorted : [Resource.Blue, Resource.Yellow, Resource.Green, Resource.Red, Resource.Orange, Resource.Navy, Resource.Violet],
  resorcesForBronze : [Resource.Blue, Resource.Yellow, Resource.Green],
  resourceForSilver : [Resource.Bronze, Resource.Orange, Resource.Green],
  resourceForGold : [Resource.Silver, Resource.Orange, Resource.Green, Resource.Blue, Resource.Navy],
  isShadow : false
}

export const Vagabond : ArchType = {
  name : 'Vagabond',
  resourcesSorted : [Resource.Violet, Resource.Blue, Resource.Green, Resource.Red, Resource.Yellow, Resource.Orange, Resource.Navy],
  resorcesForBronze : [Resource.Violet, Resource.Blue, Resource.Green],
  resourceForSilver : [Resource.Bronze, Resource.Red, Resource.Yellow],
  resourceForGold : [Resource.Silver, Resource.Red, Resource.Yellow, Resource.Orange, Resource.Navy],
  isShadow : true
}

export const Sorcerer : ArchType = {
  name : 'Sorcerer',
  resourcesSorted : [Resource.Orange, Resource.Navy, Resource.Red, Resource.Yellow, Resource.Green, Resource.Blue, Resource.Violet],
  resorcesForBronze : [Resource.Orange, Resource.Navy, Resource.Red],
  resourceForSilver : [Resource.Bronze, Resource.Yellow, Resource.Green],
  resourceForGold : [Resource.Silver, Resource.Yellow, Resource.Green, Resource.Blue, Resource.Violet],
  isShadow : true
}

export const Murder : ArchType = {
  name : 'Murder',
  resourcesSorted : [Resource.Green, Resource.Orange, Resource.Red, Resource.Yellow, Resource.Blue, Resource.Navy, Resource.Violet],
  resorcesForBronze : [Resource.Green, Resource.Orange, Resource.Red],
  resourceForSilver : [Resource.Bronze, Resource.Yellow, Resource.Blue],
  resourceForGold : [Resource.Silver, Resource.Yellow, Resource.Blue, Resource.Navy, Resource.Violet],
  isShadow : true
}

export const Cannibal : ArchType = {
  name : 'Cannibal',
  resourcesSorted : [Resource.Red, Resource.Green, Resource.Navy, Resource.Yellow, Resource.Orange, Resource.Blue, Resource.Violet],
  resorcesForBronze : [Resource.Red, Resource.Green, Resource.Navy],
  resourceForSilver : [Resource.Bronze, Resource.Yellow, Resource.Orange],
  resourceForGold : [Resource.Silver, Resource.Yellow, Resource.Orange, Resource.Blue, Resource.Violet],
  isShadow : true
}
