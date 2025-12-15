import { ApplicationConfig, provideBrowserGlobalErrorListeners, LOCALE_ID } from '@angular/core';
import { IMAGE_CONFIG } from '@angular/common';

import { provideRouter } from '@angular/router';
import { routes } from './app.routes';

import { provideClientHydration, withIncrementalHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { registerLocaleData } from '@angular/common';

import localeEsCL from '@angular/common/locales/es-CL';
import localeEsCLExtra from '@angular/common/locales/extra/es-CL';

registerLocaleData(localeEsCL, 'es-CL', localeEsCLExtra);

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: LOCALE_ID, useValue: 'es-CL' },
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withFetch()),
    provideRouter(routes),
    provideClientHydration(withIncrementalHydration()),
    {
      provide: IMAGE_CONFIG,
      useValue: {
        disableImageSizeWarning: true,
        disableImageLazyLoadWarning: true
      }
    }
  ]
};
