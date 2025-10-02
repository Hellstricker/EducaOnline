import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CursoComponent } from "./curso.component";
import { ListaCursoComponent } from "./lista-curso/lista-curso.component";

const routes: Routes = [
  {
    path: '',
    component: CursoComponent,
    children: [
      {
        path: '',
        component: ListaCursoComponent
      }
    ]
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: [],
  providers: [],
})
export class CursoRouting { }