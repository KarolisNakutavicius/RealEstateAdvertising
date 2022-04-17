import React from 'react';
import ReactDOM from 'react-dom';
import Layout from './components/Layout';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { render } from "react-dom";
import Login from "./components/Login";
import Register from "./components/Register";
import Home from "./components/Home";

const rootElement = document.getElementById("root");
render(
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<Home />} />
        <Route path={"/home"} element={<Home />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Route>
    </Routes>
  </BrowserRouter>,
  rootElement
);