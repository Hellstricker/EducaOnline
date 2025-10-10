import { Component, inject, OnInit } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";

@Component({
  selector: 'app-lista-usuario',
  templateUrl: 'lista-usuario.component.html',
  styleUrls: ['lista-usuario.component.scss'],
  standalone: false
})
export class ListaUsuarioComponent implements OnInit {
  readonly dialog = inject(MatDialog);
  private _snackBar = inject(MatSnackBar);

  displayedColumns: string[] = ['nome', 'dataCadastro', 'ativo', 'acoes'];

  usuarios: any[] = [];

  constructor(

  ) {
  }

  ngOnInit() {

  }
}
