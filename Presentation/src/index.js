import React from 'react';
import {render} from 'react-dom';
import Layout from './Components/General/Layout';
import {BrowserRouter} from "react-router-dom";

const rootElement = document.getElementById("root");
render(
    <BrowserRouter>
        <Layout/>
    </BrowserRouter>,
    rootElement
);