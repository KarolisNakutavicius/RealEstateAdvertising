import React, {useRef, useState} from 'react'
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import ValidationHelper from '../../Helpers/ValidationHelper'
import AuthService from '../../Services/AuthService'

export default function Login(props) {

    const formRef = useRef();
    const checkBtnRef = useRef();

    const [pageInfo, setPageInfo] = useState({
        loading: false,
        message: "",
        successful: false
    });

    const [loginInfo, setLoginInfo] = useState({
        email: "",
        password: "",
    })

    function handleLogin(e) {
        e.preventDefault();

        setPageInfo({
            message: "",
            loading: true,
            successful: false
        })
        debugger;
        formRef.current.validateAll();
        if (checkBtnRef.current.context._errors.length === 0) {
            AuthService.login(loginInfo.email, loginInfo.password).then(
                response => {
                    debugger;
                    props.authenticationUpdated();
                    setPageInfo({
                        loading: false,
                        message: response,
                        successful: true
                    });
                },
                error => {
                    console.log(error);
                    setPageInfo({
                        loading: false,
                        message: error,
                        successful: false
                    })
                }
            );
        } else {
            setPageInfo({
                ...pageInfo,
                loading: false,
            })
        }
    }

    return (
        <div className="col-md-12">
            <div className="card card-container">
                <img
                    src="//ssl.gstatic.com/accounts/ui/avatar_2x.png"
                    alt="profile-img"
                    className="profile-img-card"
                />
                <Form
                    onSubmit={handleLogin}
                    ref={formRef}
                >
                    <div className="form-group">
                        <label htmlFor="Email">Email</label>
                        <Input
                            type="text"
                            className="form-control"
                            name="Email"
                            value={loginInfo.email}
                            onChange={(e) => setLoginInfo({...loginInfo, email: e.target.value})}
                            validations={[ValidationHelper.required]}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <Input
                            type="password"
                            className="form-control"
                            name="password"
                            value={loginInfo.password}
                            onChange={(e) => setLoginInfo({...loginInfo, password: e.target.value})}
                            validations={[ValidationHelper.required]}
                        />
                    </div>
                    <div className="form-group">
                        <CheckButton
                            className="btn btn-primary btn-block mt-3"
                            ref={checkBtnRef}
                        >Log in</CheckButton>
                    </div>
                    {pageInfo.message && (
                        <div className="form-group mt-3">
                            <div
                                className={
                                    pageInfo.successful
                                        ? "alert alert-success"
                                        : "alert alert-danger"
                                }
                                role="alert"
                            >
                                {pageInfo.message}
                            </div>
                        </div>
                    )}
                </Form>
            </div>
        </div>
    );
}