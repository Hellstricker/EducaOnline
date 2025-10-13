import { Injectable } from "@angular/core";
import { environment } from "../configuration";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { AlunoResponseModel, MatriculaResponseModel } from "@educa-online/data";

@Injectable()
export class AlunoService {

  apiBase: string;

  constructor(
    private httpCliente: HttpClient
  ) { 
    this.apiBase = `${environment.apiAluno}/Alunos`;
  }

  getMatriculas(id: string): Observable<MatriculaResponseModel[]> {
    return this.httpCliente.get<MatriculaResponseModel[]>(`${this.apiBase}/${id}/matricula`);
  }

  getAll(): Observable<AlunoResponseModel[]> {
    return this.httpCliente.get<AlunoResponseModel[]>(`${this.apiBase}`);
  }
}
