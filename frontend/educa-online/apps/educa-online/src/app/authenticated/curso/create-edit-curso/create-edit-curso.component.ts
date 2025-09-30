import { Component, inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { CursoFormGroup } from "@educa-online/forms";

@Component({
  selector: 'app-create-edit-curso',
  templateUrl: './create-edit-curso.component.html',
  styleUrls: ['create-edit-curso.component.scss'],
  standalone: false
})

export class CreateEditCursoComponent implements OnInit {
  form: CursoFormGroup;

  dialogRef = inject(MatDialogRef<CreateEditCursoComponent>)
  data = inject(MAT_DIALOG_DATA) as any;

  constructor(

  ) {
    this.form = new CursoFormGroup();
  }

  ngOnInit() {
    if(this.data.id) {
      // Buscar informações curso
    }
  }

  salvar(): void {
    // const { valid, value } = this.form;

    // if(valid && this.data?.id) {
    //   // Chamar edição
    // } else if(valid) {
    //   // Chamar cadastro
    // }
  }

  voltar(): void {
    this.dialogRef.close(false);
  }
}
