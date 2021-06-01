import react from 'react';
import {BrowserRouter, Route, Switch} from 'react-router-dom';

import Arquivos from './pages/Arquivos'
import Arquivo from './pages/Arquivo'

export default function Routes(){
    /*BrowserRouter => garante o rotiamento correto
     Switch => garante que não vai ter mais de uma rota aberta ao mesmo tempo
     Route => define a rota*/
    return(
        <BrowserRouter>
        <Switch>   
            <Route path="/" exact component={Arquivos} ></Route>
            <Route path="/arquivo/:id" component={Arquivo}></Route>
        </Switch>
        </BrowserRouter>
    );
}