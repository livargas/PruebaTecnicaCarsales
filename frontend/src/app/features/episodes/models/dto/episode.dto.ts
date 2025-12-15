import { CharacterDTO } from "./character.dto";

export interface EpisodeDTO {
  id: number;
  name: string;
  air_date: string;
  episode: string;
  characters: string[];
  fullCharacters: CharacterDTO[];
  url: string;
  created: string;
}
