import React, { Component } from 'react'
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import ValidationHelper from '../Helpers/ValidationHelper'
import AdvertisementService from '../Services/AdvertisementService';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default class AddAdvertisement extends Component {

  constructor(props) {
    super(props);

    this.handleCreate = this.handleCreate.bind(this);
    this.onChangeName = this.onChangeName.bind(this);
    this.onPurposeChange = this.onPurposeChange.bind(this);
    this.onTypeChange = this.onTypeChange.bind(this);
    this.onSizeChange = this.onSizeChange.bind(this);
    this.onCityChange = this.onCityChange.bind(this);
    this.onStreetChange = this.onStreetChange.bind(this);
    this.onNumberChange = this.onNumberChange.bind(this);
    this.onDescriptionChange = this.onDescriptionChange.bind(this);
    this.onZipChange = this.onZipChange.bind(this);
    this.onPriceChange = this.onPriceChange.bind(this);
    this.onSelectedImageChange = this.onSelectedImageChange.bind(this)
  

    this.state = {
      name: "Geras namas Vilniaus centre",
      purpose: 0,
      type: 1,
      size: 44,
      city: "Vilnius",
      number: 4,
      street: "Latvių g.",
      zip: "086111",
      description: "Nu jau žiauriai puikus namelis yra čia",
      price:260000,
      successful: false,
      selectedImages: null,
      message: ""
    };
  }

  handleCreate(e) {
    e.preventDefault();

    this.form.validateAll();
    if (this.checkBtn.context._errors.length !== 0) {
      return;
    }

    var request = new FormData();
    request.append('Name', this.state.name)
    request.append('IsRent', !!this.state.purpose)
    request.append('Type', this.state.type)
    request.append('Size', this.state.size)
    request.append('City', this.state.city)
    request.append('Number', this.state.number)
    request.append('Zip', this.state.zip)
    request.append('Price', this.state.price)
    request.append('Description', this.state.description)
    request.append('Files', this.state.selectedImages[0])
    

    AdvertisementService.createNewAdvertisement(request).then(
      response => {
        this.setState({
          message: response,
          successful: true
        });
      },
      error => {
        this.setState({
          message: error,
          successful: false
        });
      }
    )
  }

  onChangeName(e) {
    this.setState(
      {
        name: e.target.value
      });
  }

  onPurposeChange(e) {
    this.setState(
      {
        purpose: e.target.value
      });
  }

  onTypeChange(e) {
    this.setState(
      {
        type: parseInt(e.target.value)
      });
  }

  onSizeChange(e) {
    this.setState(
      {
        size: e.target.value
      });
  }

  onCityChange(e) {
    this.setState(
      {
        city: e.target.value
      });
  }

  onStreetChange(e) {
    this.setState(
      {
        street: e.target.value
      });
  }

  onNumberChange(e) {
    this.setState(
      {
        number: e.target.value
      });
  }

  onDescriptionChange(e) {
    this.setState(
      {
        description: e.target.value
      });
  }

  onZipChange(e) {
    this.setState(
      {
        zip: e.target.value
      });
  }

  onPriceChange(e)
  {
    this.setState(
      {
        price: e.target.value
      });
  }

  onSelectedImageChange(e)
  {
    this.setState(
      {
        selectedImages: e.target.files
      });
  }

  render() {
    return (
      <div className="col-md-12">
        <div className="card card-container" style={{ minWidth: "700px" }}>
          <Form
            onSubmit={this.handleCreate}
            ref={c => {
              this.form = c;
            }}>
            {!this.state.successful && (
              <>              
                <div>
                  <div className="form-group text-center" style={{padding:"0 100px 0 100px"}}>
                    <h5 className='mt-3'>Title</h5>
                    <Input                      
                      type="text"
                      className="form-control"
                      name="text"
                      value={this.state.name}
                      onChange={this.onChangeName}
                      validations={[ValidationHelper.required]}
                    />
                  </div>

                  <h4 className='mt-4'>Information</h4>

                  <div className="d-flex justify-content-between">
                    <div className="form-group">
                      <label htmlFor="Type">Purpose</label>
                      <select className='form-select' onChange={this.onPurposeChange} value={this.state.purpose}>
                        <option value="0">Sell</option>
                        <option value="1">Rent</option>
                      </select>
                    </div>
                    <div className="form-group col-4">
                      <label htmlFor="Type">Type</label>
                      <select id='Type' className='form-select' onChange={this.onTypeChange} value={this.state.type}>
                        <option value="1">Residential</option>
                        <option value="2">Commercial</option>
                        <option selected value="3">Industrial</option>
                        <option value="4">Land</option>
                        <option value="5">SpecialPurpose</option>
                      </select>
                    </div>
                    <div className="form-group col-2">
                      <label htmlFor='size'>Size (in m²)</label>
                      <Input onChange={this.onSizeChange} value={this.state.size}
                        id='size'
                        type="number" min="1"
                        className="form-control"
                        validations={[ValidationHelper.required]}
                      />
                    </div>
                    <div className="form-group col-2">
                      <label htmlFor='price'>Price (in €)</label>
                      <Input onChange={this.onPriceChange} value={this.state.price}
                        type="number" min="1" step="0.01"
                        className="form-control"
                        id='price'
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
                          value={this.state.city}
                          onChange={this.onCityChange}
                          validations={[ValidationHelper.required]}
                        />
                      </div>
                    </div>

                    <div className='form-group col-4'>
                      <label for="street">Street</label>
                      <Input
                        id="street"
                        value={this.state.street}
                        onChange={this.onStreetChange}
                        type="text"
                        className="form-control"
                        validations={[ValidationHelper.required]}
                      />
                    </div>

                    <div className='form-group col-2'>
                      <label htmlFor="streetNumber">Number</label>
                      <Input
                        id="streetNumber"
                        value={this.state.number}
                        onChange={this.onNumberChange}
                        type="number"
                        className="form-control"
                      />
                    </div>

                    <div className='form-group col'>
                    <label htmlFor="zip">Zip</label>
                    <Input
                      value={this.state.zip}
                      onChange={this.onZipChange}
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
                      value={this.state.description}
                      onChange={this.onDescriptionChange}
                      type="text"
                      className="form-control"
                      name="text"
                      validations={[ValidationHelper.required]}
                    />
                  </div>

                  <div class="form-group">
                  <label htmlFor='image'>
                      <h4 className='mt-4'>Images</h4>
                    </label>
                    <Input type="file" className="form-control-file mt-2" id="image" onChange={this.onSelectedImageChange}/>
                  </div>

                  <div className="form-group mt-4">
                    <CheckButton className="btn btn-primary btn-block"
                      ref={c => {
                        this.checkBtn = c;
                      }}
                    >Create
                    </CheckButton>
                  </div>
                </div>
              </>
            )}
            {this.state.message && (
              <div className="form-group mt-3">
                <div
                  className={
                    this.state.successful
                      ? "alert alert-success"
                      : "alert alert-danger"
                  }
                  role="alert"
                >
                  {this.state.message}
                </div>
              </div>
            )}
          </Form>
        </div>
      </div>
    )
  }
}
