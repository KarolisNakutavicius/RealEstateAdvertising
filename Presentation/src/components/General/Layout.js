import React, {useState, useRef, useEffect} from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import '../../Styles/index.css'
import '../../Styles/Layout.css'
import Topbar from './Topbar';
import Login from "../Onboarding/Login";
import Register from "../Onboarding/Register";
import Home from "../Home/Home";
import AddAdvertisement from '../Advertisements/AddAdvertisement';
import MyAdvertisements from '../Advertisements/MyAdvertisements';
import {Navigate, Route, Routes} from "react-router-dom";
import AuthService from '../../Services/AuthService';
import {ErrorBoundary} from 'react-error-boundary'

export default function Layout() {

    const [currentUser, setCurrentUser] = useState(undefined);
    const homeRef = useRef();

    useEffect(() => {
        handleAuthenticationUpdated()
    },[])

    function handleAuthenticationUpdated() {
        setCurrentUser(AuthService.getCurrentUser())
        // debugger;
        // homeRef.current();
    }

    function ErrorFallback({error, resetErrorBoundary}) {
        return (
            <div role="alert">
                <p>Something went wrong:</p>
                <pre>{error.message}</pre>
                <button onClick={resetErrorBoundary}>Try again</button>
            </div>
        )
    }

    return (
        <div>
            <Topbar authenticationUpdated={handleAuthenticationUpdated}/>
            <div className="container mt-3">
                <Routes>
                    <Route index element={<Home ref={homeRef}/>}/>
                    <Route path={"/home"} element={<Home ref={homeRef}/>}/>
                    <Route path={"/my-advertisements"} element={
                        currentUser
                            ? (
                                <ErrorBoundary FallbackComponent={ErrorFallback}>
                                    <MyAdvertisements/>
                                </ErrorBoundary>
                            )
                            : (<Navigate to="/home" replace/>)}/>
                    <Route path={"/add-advertisement"} element={
                        currentUser
                            ? (
                                <ErrorBoundary FallbackComponent={ErrorFallback}>
                                    <AddAdvertisement/>
                                </ErrorBoundary>
                            )
                            : (<Navigate to="/home" replace/>)}/>
                    <Route path="*" element={<Navigate to="/home" replace/>}/>
                    <Route path="/login" element={
                        !currentUser
                            ? (
                                <ErrorBoundary FallbackComponent={ErrorFallback}>
                                    <Login authenticationUpdated={handleAuthenticationUpdated}/>
                                </ErrorBoundary>
                            )
                            : (<Navigate to="/home" replace/>)}/>
                    <Route path="/register" element={
                        !currentUser
                            ? (
                                <ErrorBoundary FallbackComponent={ErrorFallback}>
                                    <Register authenticationUpdated={handleAuthenticationUpdated}/>
                                </ErrorBoundary>
                            )
                            : (<Navigate to="/home" replace/>)}/>
                </Routes>
            </div>
        </div>
    )
}
