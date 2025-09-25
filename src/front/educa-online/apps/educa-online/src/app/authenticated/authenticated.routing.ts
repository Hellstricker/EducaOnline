import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticatedComponent } from './authenticated.component';
import { AuthenticatedGuard } from '../guards/authenticated.guard';

const routes: Routes = [
    {
      path: '',
      component: AuthenticatedComponent,
      // canActivate: [AuthenticatedGuard],
      children: [
        {
          path: '',
          pathMatch: 'full',
          redirectTo: 'inicio',
        },
        {
          path: 'inicio',
          loadChildren: () =>
            import('./inicio/inicio.module').then((x) => x.InicioModule)
        },
        {
          path: 'usuario',
          loadChildren: () =>
            import('./usuario/usuario.module').then((x) => x.UsuarioModule)
        },
      ],
    },
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class AuthenticatedRouting { }
