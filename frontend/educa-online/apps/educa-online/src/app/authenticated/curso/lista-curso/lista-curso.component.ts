import { Component, inject, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { CreateEditCursoComponent } from "../create-edit-curso/create-edit-curso.component";
import { filter, take } from "rxjs";
import { AlertComponent, AlertOptions } from "@educa-online/components";

@Component({
  selector: 'app-lista-curso',
  templateUrl: 'lista-curso.component.html',
  styleUrls: ['lista-curso.component.scss'],
  standalone: false
})
export class ListaCursoComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  displayedColumns: string[] = ['nome', 'titulo', 'cargaHoraria', 'ativo', 'acoes'];

  cursos: any[] = [];

  constructor(

  ) {
  }

  ngOnInit() {
    this.getCursos();
  }

  getCursos(){
    // Buscar cursos
  }

  novoCurso(): void {
    const ref = this.dialog.open(CreateEditCursoComponent, {
      width: '40rem',
    });

    ref.afterClosed()
      .pipe(
        take(1),
        filter(data => data))
      .subscribe(_ => {
        this._snackBar.openFromComponent(AlertComponent, {
          duration: 5000,
          data: {
            title: 'Sucesso!',
            subtitle: 'Curso criado',
            status: 'sucesso'
          } as AlertOptions
        });

        this.getCursos();
      });
  }
}
