import React, { Component } from 'react'
import Row from 'react-bootstrap/Row';
import Input from "react-validation/build/input";
import ValidationHelper from '../Helpers/ValidationHelper'
import Form from "react-validation/build/form";
import Col from 'react-bootstrap/Col';

export default class Filters extends Component {    

    constructor(props) {
        super(props);
        this.onMaxPriceChange = this.onMaxPriceChange.bind(this)
        this.onMinPriceChange = this.onMinPriceChange.bind(this)
        this.onSelectedCityChange = this.onSelectedCityChange.bind(this)

        this.state =
        {
            minPrice: 1000,
            maxPrice: 500000,
            selectedCity:1,
            cities:[],
            message: ""
        }
    }

    componentDidMount() {
        fetch("/api/Cities").then(response => {
            response.json().then(data => {
                this.setState({
                    cities: data
                })
            })
        })
    }

    onMinPriceChange(e) {
        this.setState({
            minPrice: parseInt(e.target.value)
        })

    }

    onMaxPriceChange(e) {
        this.setState({
            maxPrice: parseInt(e.target.value)
        })
    }

    onSelectedCityChange(e){
        this.setState({
            selectedCity: parseInt(e.target.value)
        })
    }

    filterDown(e){

        if(this.state.minPrice > this.state.maxPrice){
            this.setState({
                message:'Min price cannot be higher thet max price'
            })
            return;
        }

        this.props.filtersChanged();

        this.setState({
            message:''
        })
    }

    render() {
        return (
            <div style={{position:'relative', backgroundColor:'rgba(210, 215,211,1)', borderRadius:'6px', textAlign:'center'}}>

                <h3>Filters</h3>

                <Form className="p-3">

                    <div className='d-flex'>
                    <div className='p-2 filter-block'>
                        <h5>Price</h5>
                        <Row className='p-2'>
                            <label for="from" className="col-3">From:</label>
                            <div className='col'>
                                <Input
                                    min="0" max="999999999999" step="1000"
                                    type="number"
                                    className="col-1 mt-0 form-control"
                                    id="from"
                                    value={this.state.minPrice}
                                    onChange={this.onMinPriceChange}
                                />
                            </div>
                        </Row>
                        <Row className='p-2'>
                            <label for="to" className="col-3">To:</label>
                            <div className='col'>
                                <Input
                                    min="5000" max="999999999999" step="1000"
                                    type="number"
                                    className="col-1 mt-0 form-control"
                                    id="to"
                                    value={this.state.maxPrice}
                                    onChange={this.onMaxPriceChange}
                                />
                            </div>
                        </Row>
                    </div>
                    <div className='p-2 filter-block'>
                        <h4 >City</h4>
                        <select className='mt-4 form-select' onChange={this.onSelectedCityChange} value={this.state.selectedCity}>
                            <option value='0'>All</option>
                            {this.state.cities.map(c => <option value={c.id}>{c.name}</option>)}
                        </select>
                    </div>
                    </div>                
                </Form>

                {this.state.message && (
                    <div className="form-group mt-3">
                        <div className="alert alert-danger"role="alert">
                            {this.state.message}
                        </div>
                    </div>
                )}

                <button type="button" className="mb-3 btn btn-primary" onClick={this.filterDown.bind(this)}>Filter down</button>

            </div>
        )
    }
}
