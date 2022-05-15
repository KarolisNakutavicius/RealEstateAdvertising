import React, {Component} from 'react'
import AdvertisementService from '../Services/AdvertisementService'
import Advertisement from './Advertisment';
import Filters from './Filters';
import Collapse from "react-bootstrap/Collapse";
import Button from 'react-bootstrap/Button'

export default class Home extends Component {

    constructor(props) {
        super(props);
        this.onFiltersChange = this.onFiltersChange.bind(this)
        this.handleOpen = this.handleOpen.bind(this)

        this.filtersRef = React.createRef()

        this.state = {
            advertisements: [],
            message: 'Loading ...',
            open: false
        }
    }

    async onFiltersChange() {
        const filters = this.filtersRef.current;

        let request = {
            MinPrice: filters.state.minPrice,
            MaxPrice: filters.state.maxPrice,
        }

        if (filters.state.selectedCity > 0) {
            request.CityId = filters.state.selectedCity
        }

        await this.getAds(request)
    }

    async getAds(request) {
        let ads = await AdvertisementService.getAllAdvertisments(request);

        if (ads.length > 0) {
            this.setState({
                advertisements: ads,
                message: "",
            })

            return;
        }

        this.setState({
            message: "There are no advertisements posted yet",
            advertisements: []
        })
    }

    handleOpen() {
        this.setState({
            open: !this.state.open
        })
    }

    componentDidMount() {
        this.getAds();
    }

    render() {
        return (
            <>
                <Button
                    onClick={this.handleOpen}
                    aria-controls="dropdown"
                    aria-expanded={this.state.open}
                >
                    Open filters
                    <i className={!this.state.open ? "fa fa-angle-double-down" : "fa fa-angle-double-up"}
                       style={{fontSize: '24px', margin: "3px 0 0 6px"}}></i>
                </Button>
                <Collapse in={this.state.open}>
                    <div className='mt-1' id="dropdown">
                        <Filters ref={this.filtersRef} filtersChanged={this.onFiltersChange}/>
                    </div>
                </Collapse>


                {this.state.advertisements.length === 0 && (
                    <h3>{this.state.message}</h3>
                )}
                <div className='mt-4 d-flex justify-content-start flex-wrap'>
                    {this.state.advertisements.map(ad => {
                        return <Advertisement ad={ad} isPersonal={true}/>
                    })}
                </div>
            </>
        )
    }
}
