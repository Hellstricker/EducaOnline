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
          path: 'usuario',
          loadChildren: () =>
            import('./usuario/usuario.module').then((x) => x.UsuarioModule)
        },
        {
          path: 'curso',
          loadChildren: () =>
            import('./curso/curso.module').then((x) => x.CursoModule)
        },
        {
          path: 'matricula/:cursoId',
          loadChildren:() => 
            import('./matricula/matricula.module').then((x) => x.MatriculaModule)
        }
      ],
    },
  ];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})

export class AuthenticatedRouting { }
