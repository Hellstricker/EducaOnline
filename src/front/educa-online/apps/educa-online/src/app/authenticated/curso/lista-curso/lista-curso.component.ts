import { Component, inject, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";

@Component({
  selector: 'app-lista-curso',
  templateUrl: 'lista-curso.component.html',
  styleUrls: ['lista-curso.component.scss'],
  standalone: false
})
export class ListaCursoComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  displayedColumns: string[] = ['nome', 'dataCadastro', 'ativo', 'acoes'];

  cursos: any[] = [];

  constructor(

  ) {
  }

  ngOnInit() {

  }
}
