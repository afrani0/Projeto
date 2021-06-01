import react, { useState, useEffect } from 'react'
import { useHistory } from 'react-router-dom'
import axios from 'axios'

import './style.css'
import logoImage from '../../assets/1480.gif';
import { bool } from 'yup';
import Titulo from '../../componentes/Titulo/Titulo.js';

export default function Arquivos() {

    const rota = useHistory();

    const [listaArquivos, setListaArquivos] = useState(null);
    const [spinner, setSpinner] = useState(false);

    const [pesquisa, setPesquisa] = useState('');

    useEffect(() => {
        
        ListarArquivos();
    }, [listaArquivos])


    function ListarArquivos() {
        
        axios.get(`https://localhost:44313/api/arquivo?nome=${pesquisa}`)
            .then(res => {
                setListaArquivos(res.data);
            });
    }

    function Excluir(id) {
        let api = axios.create({ baseURL: "https://localhost:44313" })
        api.delete(`/api/arquivo/` + id)
            .then(res => { });
    }

    function NovoEEdicao(id) {
        debugger
        rota.push(`/arquivo/${id}`);
    }

    if (listaArquivos === null)
        return <div className="pages-arquivos-body">Carregando... <img src={logoImage}></img>  </div>
    else
        return (

            <div className="pages-arquivos-body">
                <Titulo titulo="Programa De Controle De Imagens"></Titulo>
                <div className="pages-arquivos-barrainicial">
                    <div className="pages-arquivos-pesquisa">
                        <label>Pesquisar por nome </label>
                        <input onChange={((e) => setPesquisa(e.target.value))}></input>

                    </div>
                    <div className="pages-arquivos-buttonnew">
                        <button onClick={() => NovoEEdicao(0)} >Novo Arquivo</button>
                    </div>

                </div>

                {listaArquivos.map(item => (

                    <div className="pages-arquivos-item">
                        <img className="pages-arquivos-imagem" src={item.caminhoImg} alt="" />
                        <h1 className="pages-arquivos-nome" title="Nome">{item.nome}</h1>
                        <span className="pages-arquivos-descricao" title="Descrição">{item.descricao}</span>
                        <div className="pages-arquivos-detalhes">
                            <div >Detalhes Técnicos da Imagem</div>
                            <span className="pages-arquivos-tamanho">Tamanho: {item.tamanho}</span>
                            <span className="pages-arquivos-formato">Formato: {item.formato}</span>
                        </div>

                        <div className="pages-arquivos-button">
                            <button type="submit" onClick={() => Excluir(item.arquivoId)}>Excluir</button>
                            <button onClick={() => NovoEEdicao(item.arquivoId)}>Editar</button>
                        </div>
                    </div>
                ))}
            </div>)
}