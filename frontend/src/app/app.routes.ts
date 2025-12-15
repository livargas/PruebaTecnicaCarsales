import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'episodes',
    pathMatch: 'full'
  },
  {
    path: 'episodes',
    loadComponent: () => import('@features/episodes/pages/episodes-list/episodes-list').then(m => m.EpisodesListComponent)
  }
];
