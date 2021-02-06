import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

export function geApiUrl() {
  return "http://localhost:5000/"; //document.getElementsByTagName('base')[0].href;
}

const providers = [
  { provide: 'FAMILY_CIRCLE_API_URL', useFactory: geApiUrl, deps: [] }
];

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic(providers).bootstrapModule(AppModule)
  .catch(err => console.log(err));
