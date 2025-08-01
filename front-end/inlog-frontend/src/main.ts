import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { routes } from './app/app.routes';
import { provideHttpClient } from '@angular/common/http';
import { setTheme, fabricLightTheme } from '@fabric-msft/theme';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideToastr } from 'ngx-toastr';
import 'zone.js'

setTheme(fabricLightTheme);

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(routes), 
    provideHttpClient(),
    provideAnimations(),
    provideToastr()
  ]
}).catch(err => console.error(err));