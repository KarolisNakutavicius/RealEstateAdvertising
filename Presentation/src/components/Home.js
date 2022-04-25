import React, { Component } from 'react'
import AdvertisementService from '../Services/AdvertisementService'
import Advertisment from './Advertisment';
import Filters from './Filters';

export default class Home extends Component {

  constructor(props) {
    super(props);
    this.onFiltersChange = this.onFiltersChange.bind(this)

    this.filtersRef = React.createRef()

    this.state = {
      advertisements: [],
      message: 'Loading ...'
    }
  }

  async componentDidMount() {
    await this.getAds();
  }
  
  async onFiltersChange(e){
    const filters = this.filtersRef.current;

    var request = {
      MinPrice: filters.state.minPrice,
      MaxPrice: filters.state.maxPrice,
      CityId: filters.state.selectedCity
    }

    await this.getAds(request)
  }

  async getAds(request){
    var ads = await AdvertisementService.getAllAdvertisments(request);

    if (ads.length > 0) {
      this.setState({
        advertisements: ads
      })

      return;
    }

    this.setState({
      message: "There are no advertisements posted yet",
      advertisements: []
    })
  }  

  render() {
    return (
      <>       

       <Filters ref={this.filtersRef} filtersChanged={this.onFiltersChange}/>

        {this.state.advertisements.length == 0 && (
          <h3>{this.state.message}</h3>
        )}
        <div className='mt-4 d-flex justify-content-start flex-wrap'>
          {this.state.advertisements.map(ad => {
            return <Advertisment ad={ad} isPersonal={true} />
          })}
        </div>
      </>
    )
  }
}
