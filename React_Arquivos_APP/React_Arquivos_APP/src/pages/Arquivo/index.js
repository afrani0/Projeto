import react from 'react'
import { Link, useHistory, useParams } from 'react-router-dom'
import { useEffect, useState } from 'react/cjs/react.development';

import logoImage from '../../assets/1480.gif';
import axios from 'axios'

import Titulo from '../../componentes/Titulo/Titulo.js'

import { FiArrowLeft } from 'react-icons/fi';

import './style.css';
import { string } from 'yup';

export default function Arquivo() {

    const { id } = useParams();

    const rota = useHistory();

    const [arquivoId, setArquivoId] = useState(-1);
    const [nome, setNome] = useState('');
    const [descricao, setDescricao] = useState('');
    const [imagem, setImagem] = useState(null);

    const [imagemURL, setImagemURL] = useState('');

    const initialError = {
        descricao: '',
        imagem: '',
        nome: '',
        arquivoId: 0
    }

    const [error, setError] = useState(initialError);



    useEffect(() => {
        // Atualiza o titulo do documento usando a API do browser

        if (id == 0) {
            setArquivoId(0);
            return;
        } else {
            carregarDados();
        }

    }, id);


    async function carregarDados() {

        await axios.get(`https://localhost:44313/api/arquivo/${id}`)
            .then(res => {
                let dadosIniciais = res.data;
                setNome(dadosIniciais.nome);
                setDescricao(dadosIniciais.descricao)
                setImagem(dadosIniciais.caminhoImg)
                setArquivoId(dadosIniciais.arquivoId)


            }).catch((error) => {

                let messageErro = error.response.data;
                setError(messageErro)
            })
    }

    async function salvar(e) {
        console.log(e.form);
        e.preventDefault();

        const data = {
            ArquivoId: arquivoId,
            Nome: nome,
            Descricao: descricao,
            Imagem: imagem,
        }
        /*Para conseguir passar os valores por form deve ser criado um 'FormData()' e 
        dar append em cada campo com (chave e valor) conforme abaixo*/
        var formData = new FormData();

        formData.append("imagem", imagem);
        formData.append("nome", nome);
        formData.append("descricao", descricao);
        formData.append("arquivoId", arquivoId);

        console.log("maga")
        console.log(data)
        console.log(arquivoId)

        if (arquivoId === 0) {
            await axios.create({
                baseURL: 'https://localhost:44313'
                ,
                headers: {/*mesmo que omita o 'headers', ainda assim vai funcionar o envio de dados no formato 'FormData'*/
                    'Content-Type': 'multipart/form-data'
                }
            }).post("api/arquivo", formData)
                .then(res => {
                    console.log('acerto');
                    rota.push("/");
                }).catch((error) => {

                    if (typeof error.response.data === 'string') {
                        alert(error.response.data)
                        rota.push("/");
                    }
                    console.log("erro");
                    let messageErro = error.response.data;
                    console.log(messageErro.Nome);
                    setError(messageErro)
                    console.log(error.response.data) //Logs a string: Error: Request failed with status code 404
                })
        } else {
            debugger;
            if (typeof document.getElementsByTagName('img')[0] === 'undefined') {
                let imagem = { 'Imagem': 'Imagem não pode ficar vazia. Escolha uma imagem ou retorne a anterior' };
                setError(imagem);

            } else
                await axios.create({
                    baseURL: 'https://localhost:44313'
                    ,
                    headers: {/*mesmo que omita o 'headers', ainda assim vai funcionar o envio de dados no formato 'FormData'*/
                        'Content-Type': 'multipart/form-data'
                    }
                }).put(`api/arquivo/${arquivoId}`, formData)
                    .then(res => {
                        console.log('acerto');
                        rota.push("/");
                    }).catch((error) => {
                        if (typeof error.response.data === 'string') {
                            alert(error.response.data)
                            rota.push("/");
                        }
                        console.log("erro");
                        debugger
                        let messageErro = error.response.data;
                        console.log(messageErro.Nome);
                        setError(messageErro)
                        console.log(error.response.data) //Logs a string: Error: Request failed with status code 404
                    })
        }

    }

    function EditarImagem() {
        setImagem(null);
    }

    async function CancelarEditarImagem() {
        await axios.get(`https://localhost:44313/api/arquivo/${id}`)
            .then(res => {
                let dadosIniciais = res.data;
                setImagem(dadosIniciais.caminhoImg)

            }).catch((error) => {

                let messageErro = error.response.data;
                setError(messageErro)
            })
        setImagemURL('');
        return;
    }

    function setarImagemEPreview(e) {

        if (e.target.files && e.target.files[0]) {
            let reader = new FileReader();
            reader.onload = function (ev) {
                setImagemURL(ev.target.result);
            }.bind(this);
            reader.readAsDataURL(e.target.files[0]);
        }

        setImagem(e.target.files[0]);
    }

    if (arquivoId === -1)
        return <div className="pages-arquivos-body">Carregando... <img src={logoImage}></img>  </div>
    else
        return (
            <div className="pages-arquivos-body">
                {(id === '0')
                ?<Titulo titulo="Criando Arquivo"></Titulo>  
                :<Titulo titulo="Editando Arquivo"></Titulo> }
                <div className="pages-arquivo-registro">

                    <form onSubmit={salvar}>
                        <input type="hidden" name="arquivoId" value="{arquivoId}" ></input>
                        <div className="pages-arquivo-coluna">
                            <label from="caminhoImg" values>Caminho Imagem : </label>
                            
                                {
                                    (id == 0)
                                        ?
                                        (
                                            <div className="pages-arquivos-coluna-imagemdiv" style={{height: 210}}>
                                                <div>
                                                    <input className="pages-arquivos-coluna-imagem" name="imagem" onChange={(e) => setarImagemEPreview(e)} type="file" />
                                                    <div className="pages-arquivos-previsualizacao-imagem">
                                                        <label>Pré-visualização da imagem: </label>
                                                        <div>{imagemURL !== '' && (<img className="pages-arquivos-coluna-img" src={imagemURL} ></img>)}</div>
                                                    </div>

                                                </div>
                                            </div>

                                        )
                                        :
                                        (imagem !== null)
                                            ? (
                                                <div className="pages-arquivos-coluna-imagemdiv">
                                                    <div className="pages-arquivos-coluna-img-posicionamento">
                                                        {imagemURL === '' && (
                                                            <div>
                                                                <label>Imagem Atual:</label>
                                                                <img className="pages-arquivos-coluna-img" src={imagem}></img>
                                                            </div>
                                                        )}
                                                        {imagemURL !== '' && (
                                                            <div className="pages-arquivos-previsualizacao-imagem">
                                                                <label>Pré-visualização nova imagem: </label>
                                                                <img className="pages-arquivos-coluna-img" src={imagemURL} ></img>
                                                            </div>
                                                        )}
                                                        <di>
                                                            <button onClick={() => EditarImagem()}>Mudar Imagem</button>

                                                        </di>
                                                    </div>
                                                </div>
                                            )
                                            : (
                                                <div className="pages-arquivos-coluna-imagemdiv">
                                                    <div>
                                                        <input className="pages-arquivos-coluna-imagem" name="imagem" onChange={(e) => setarImagemEPreview(e)} type="file" />
                                                        {id != 0 ? (<button onClick={() => CancelarEditarImagem()} type="button">Cancelar</button>) : ''}

                                                    </div>
                                                </div>
                                            )
                                }

                            


                        </div>
                        {error.Imagem && (<span>{error.Imagem}</span>)}
                        <div className="pages-arquivo-coluna">
                            <label from="nome">Nome : </label>
                            <input value={nome} onChange={(e) => setNome(e.target.value)}></input>

                        </div>
                        {error.Nome && (<span>{error.Nome}</span>)}
                        <div className="pages-arquivo-coluna">
                            <label from="descricao">Descricao : </label>
                            <input value={descricao} onChange={(e) => setDescricao(e.target.value)}></input>
                        </div>
                        {error.Descricao && (<span>{error.Descricao}</span>)}

                        <div>
                            <div className="pages-arquivo-button">
                                <button className="pages-arquivo-linkVoltar" type="submit" >Salvar</button>
                                <Link className="pages-arquivo-linkVoltar" to="/" >
                                    <FiArrowLeft color="#251fc5" size={15} ></FiArrowLeft>Voltar
                        </Link>
                            </div>
                        </div>

                    </form>

                </div>
            </div>
        );
}