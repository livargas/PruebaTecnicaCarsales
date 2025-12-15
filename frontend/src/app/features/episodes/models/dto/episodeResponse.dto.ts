import { EpisodeDTO } from "./episode.dto";

export interface EpisodeResponseDTO {
  currentPage: number;
  totalPages: number;
  totalItems: number;
  episodes: EpisodeDTO[];
}