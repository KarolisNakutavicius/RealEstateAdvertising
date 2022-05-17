import React, {useState, useRef} from 'react'
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import ValidationHelper from '../../Helpers/ValidationHelper'
import AdvertisementService from '../../Services/AdvertisementService';
import Row from 'react-bootstrap/Row';

export default function AddAdvertisement() {

    const [formInfo, setFormInfo] = useState(
        {
            name: "Geras namas Vilniaus centre",
            purpose: 0,
            type: 1,
            size: 44,
            plotSize:30,
            city: "Vilnius",
            number: 4,
            street: "Latvių g.",
            zip: "086111",
            description: "Nu jau žiauriai puikus namelis yra čia",
            price: 260000,
            selectedImages: null,
        })
    
    const [successState, setSuccessState] = useState({
        successful: false,
        message: ""
    })
    
    const formRef = useRef();
    const checkBtnRef = useRef();
    
    function handleCreate(e) {
        e.preventDefault();

        formRef.current.validateAll();
        
        if (checkBtnRef.current.context._errors.length !== 0) {
            return;
        }

        if (!formInfo.selectedImages) {
            setSuccessState({
                message: 'You must add images',
                successful: false
            })
        }

        let request = new FormData();
        request.append('Name', formInfo.name)
        request.append('IsRent', !!formInfo.purpose)
        request.append('Type', formInfo.type)
        request.append('BuildingSize', formInfo.size)
        request.append('City', formInfo.city)
        request.append('Number', formInfo.number)
        request.append('Street', formInfo.street)
        request.append('Zip', formInfo.zip)
        request.append('Price', formInfo.price)
        request.append('Description', formInfo.description)
        request.append('PlotSize', formInfo.plotSize)
        request.append('Files', formInfo.selectedImages[0])

        AdvertisementService.createNewAdvertisement(request).then(
            response => {
                setSuccessState({
                    message: response,
                    successful: true
                })
            },
            error => {
                setSuccessState({
                    message: error,
                    successful: false
                })
            }
        )
    }

    return (
        <div className="col-md-12">
            <div className="card card-container" style={{minWidth: "700px"}}>
                <Form
                    onSubmit={handleCreate}
                    ref={formRef}
                >
                    {!successState.successful && (
                    <>
                        <div>
                            <div className="form-group text-center" style={{padding: "0 100px 0 100px"}}>
                                <h5 className='mt-3'>Title</h5>
                                <Input
                                    type="text"
                                    className="form-control"
                                    name="text"
                                    value={formInfo.name}
                                    onChange={(e) => setFormInfo({...formInfo, name: e.target.value})}
                                    validations={[ValidationHelper.required]}
                                />
                            </div>
                        </div>


                        <h4 className='mt-4'>Information</h4>

                        <div className="d-flex justify-content-between">
                            <div className="form-group">
                                <label htmlFor="Type">Purpose</label>
                                <select className='form-select'
                                        onChange={(e) => setFormInfo({...formInfo, purpose: e.target.value})}
                                        value={formInfo.purpose}>
                                    <option value="0">Sell</option>
                                    <option value="1">Rent</option>
                                </select>
                            </div>
                            <div className="form-group col-4">
                                <label htmlFor="Type">Type</label>
                                <select id='Type' className='form-select'
                                        onChange={(e) => setFormInfo({...formInfo, type: e.target.value})}
                                        value={formInfo.type}>
                                    <option value="1">Residential</option>
                                    <option value="2">Commercial</option>
                                    <option selected value="3">Industrial</option>
                                    <option value="4">Land</option>
                                    <option value="5">SpecialPurpose</option>
                                </select>
                            </div>
                            <div className="form-group col-2">
                                <label htmlFor='price'>Price (in €)</label>
                                <Input onChange={(e) => setFormInfo({...formInfo, price: e.target.value})}
                                       value={formInfo.price}
                                       type="number" min="1" step="0.01"
                                       className="form-control"
                                       id='price'
                                       validations={[ValidationHelper.required]}
                                />
                            </div>
                        </div>

                        <h4 className='mt-4'>Size </h4>

                        <div className="d-flex justify-content-start">
                            <div className="form-group col-4">
                                <label htmlFor='size'>Building Size (in m²)</label>
                                <Input onChange={(e) => setFormInfo({...formInfo, size: e.target.value})}
                                       value={formInfo.size}
                                       id='size'
                                       type="number" min="0"
                                       className="form-control"
                                       validations={[ValidationHelper.required]}
                                />
                            </div>

                            <div className="form-group col-4 mx-4">
                                <label htmlFor='size'>Plot Size (in m²)</label>
                                <Input onChange={(e) => setFormInfo({...formInfo, plotSize: e.target.value})}
                                       value={formInfo.plotSize}
                                       id='size'
                                       type="number" min="0"
                                       className="form-control"
                                       validations={[ValidationHelper.required]}
                                />
                            </div>
                        </div>

                        
                        
                                    <h4 className='mt-4'>Address</h4>
                        
                                    <Row>
                                        <div className='form-group col-3'>
                                            <label htmlFor="city">City</label>
                                            <div>
                                                <Input
                                                    id="city"
                                                    type="text"
                                                    className="form-control"
                                                    onChange={(e) => setFormInfo({...formInfo, city: e.target.value})}
                                                    value={formInfo.city}
                                                    validations={[ValidationHelper.required]}
                                                />
                                            </div>
                                        </div>
                        
                                        <div className='form-group col-4'>
                                            <label htmlFor='street' >Street</label>
                                            <Input
                                                id="street"
                                                onChange={(e) => setFormInfo({...formInfo, street: e.target.value})}
                                                value={formInfo.street}
                                                type="text"
                                                className="form-control"
                                                validations={[ValidationHelper.required]}
                                            />
                                        </div>
                        
                                        <div className='form-group col-2'>
                                            <label htmlFor="streetNumber">Number</label>
                                            <Input
                                                id="streetNumber"
                                                onChange={(e) => setFormInfo({...formInfo, number: e.target.value})}
                                                value={formInfo.number}
                                                type="number"
                                                className="form-control"
                                            />
                                        </div>
                        
                                        <div className='form-group col'>
                                            <label htmlFor="zip">Zip</label>
                                            <Input
                                                onChange={(e) => setFormInfo({...formInfo, zip: e.target.value})}
                                                value={formInfo.zip}
                                                type="text"
                                                className="form-control"
                                                id="zip"
                                            />
                                        </div>
                                    </Row>
                        
                                    <div className="form-group">
                                        <label htmlFor='description'>
                                            <h4 className='mt-4'>Description</h4>
                                        </label>
                                        <textarea
                                            id='description'
                                            onChange={(e) => setFormInfo({...formInfo, description: e.target.value})}
                                            value={formInfo.description}
                                            type="text"
                                            className="form-control"
                                            name="text"
                                            validations={[ValidationHelper.required]}
                                        />
                                    </div>
                        
                                    <div className="form-group">
                                        <label htmlFor='image'>
                                            <h4 className='mt-4'>Images</h4>
                                        </label>
                                        <Input type="file" className="form-control-file mt-2" id="image"
                                               onChange={(e) => setFormInfo({...formInfo, selectedImages: e.target.files})}/>
                                    </div>
                        
                                    <div className="form-group mt-4">
                                        <CheckButton className="btn btn-primary btn-block"
                                                     ref={checkBtnRef}
                                        >Create
                                        </CheckButton>
                                    </div>
                            </>
                        )}
                        {successState.message && (
                            <div className="form-group mt-3">
                                <div
                                    className={
                                        successState.successful
                                            ? "alert alert-success"
                                            : "alert alert-danger"
                                    }
                                    role="alert"
                                >
                                    {successState.message}
                                </div>
                            </div>
                        )}
                    </Form>
                </div>
            </div>
        )
}
