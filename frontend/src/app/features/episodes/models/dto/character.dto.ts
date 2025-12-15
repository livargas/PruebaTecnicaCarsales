export interface CharacterDTO {
  name: string;
  status: string;
  species: string;
  type: string;
  gender: string;
  origin: OriginDTO;
  location: LocationDTO;
  image: string;
  episode: string[];
  url: string;
  created: string;
}

export interface LocationDTO {
  name: string;
  url: string;
}

export interface OriginDTO {
  name: string;
  url: string;
}
