Namespace WeatherUndergroundData

#Region "Weather Underground Required Branding"

    ''' <summary>
    ''' A class which exposes an instance of a class which holds information on Weather Underground's required branding. Please read their documents regarding terms and conditions online at http://www.wunderground.com/weather/api/d/terms.html.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WeatherUndergroundRequired
        Private _imageUrl As String = "--"
        Private _imageTitle As String = "--"
        Private _imageLink As String = "--"

        ''' <summary>
        ''' Instantiates this class using your API key only. This will use your IP address to determine your location.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                GetRequired(apiKey, "AutoIP")
            End If
        End Sub

        Public Sub New()
        End Sub

        Private Sub GetRequired(ByVal apiKey As String, ByVal locationQry As String)

            Try
                Dim sb As New System.Text.StringBuilder

                sb.Append("http://api.wunderground.com/api/")
                sb.Append(apiKey.Trim)
                sb.Append("/conditions/q/")
                sb.Append(locationQry.Trim)
                sb.Append(".xml")

                Dim xInfo = XElement.Load(sb.ToString)

                For Each ciInfo As XElement In xInfo...<current_observation>
                    ' Required
                    For Each imgInfo As XElement In ciInfo...<image>
                        _imageUrl = imgInfo...<url>.Value
                        _imageTitle = imgInfo...<title>.Value
                        _imageLink = imgInfo...<link>.Value
                    Next
                Next

            Catch ex As Exception
                _imageUrl = Nothing
                _imageTitle = Nothing
                _imageLink = Nothing
            End Try

        End Sub

        ''' <summary>
        ''' The URL to the Weather Underground's logo image. Please read their terms for use (which is required).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WeatherUndergroundImageURL() As String
            Get
                Return _imageUrl
            End Get
        End Property

        ''' <summary>
        ''' The title to be displayed for the Weather Underground's logo image. Please read their terms for use (which is required).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WeatherUndergroundImageTitle() As String
            Get
                Return _imageTitle
            End Get
        End Property

        ''' <summary>
        ''' The URL of the link which the Weather Underground's logo image is supposed to link to. Please read their terms for use (which is required).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WeatherUndergroundImageLink() As String
            Get
                Return _imageLink
            End Get
        End Property
    End Class

#End Region



#Region "Main Classes"

    ''' <summary>
    ''' A class which exposes instances of various other classes which contain the specific data for the current conditions.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CurrentConditions
        Private _currentWeather As String = "--"
        Private _displayLocation As DisplayLocation = Nothing
        Private _observationLocation As ObservationLocation = Nothing
        Private _observationTime As ObservationTime = Nothing
        Private _temp As Temperature = Nothing
        Private _rh As RelativeHumidity = Nothing
        Private _wind As Wind = Nothing
        Private _pressure As Pressure = Nothing
        Private _dp As DewPoint = Nothing
        Private _hi As HeatIndex = Nothing
        Private _wc As WindChill = Nothing
        Private _feelsLikeTemp As FeelsLikeTemperature = Nothing
        Private _visibility As Visibility = Nothing
        Private _precip As Precipitation = Nothing
        Private _icon As WeatherIcon = Nothing

        ''' <summary>
        ''' Instantiates this class using your API key only. This will use your IP address to determine your location.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                GetConditions(apiKey, "AutoIP")
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key and either your zip code/postal code or an airport code.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="zipCodeOrAirportCode">The zip code/postal code or an airport code.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal zipCodeOrAirportCode As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                GetConditions(apiKey, zipCodeOrAirportCode)
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key, your city's name and either your state's name or the country's name.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="cityName">The name of the city (not case-sensitive).</param>
        ''' <param name="stateNameOrCountryName">The name of the state or country (not case-sensitive).</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal cityName As String,
                       ByVal stateNameOrCountryName As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                Dim sb As New System.Text.StringBuilder

                sb.Append(stateNameOrCountryName)
                sb.Append("/")
                sb.Append(cityName)

                GetConditions(apiKey, sb.ToString)
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key and a latitude/longitude pair. Note that the latitude and longitude are type Double, not type String!
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="latitude">The latitude (type Double).</param>
        ''' <param name="longitude">The longitude (type Double).</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal latitude As Double,
                       ByVal longitude As Double)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                Dim sb As New System.Text.StringBuilder

                sb.Append(latitude.ToString)
                sb.Append(",")
                sb.Append(longitude.ToString)

                GetConditions(apiKey, sb.ToString)
            End If

        End Sub

        Private Sub GetConditions(ByVal apiKey As String, ByVal locationQry As String)

            Try
                Dim sb As New System.Text.StringBuilder

                sb.Append("http://api.wunderground.com/api/")
                sb.Append(apiKey.Trim)
                sb.Append("/conditions/q/")
                sb.Append(locationQry.Trim)
                sb.Append(".xml")

                Dim xInfo = XElement.Load(sb.ToString)

                For Each ciInfo As XElement In xInfo...<current_observation>

                    ' Display Location
                    For Each displayInfo As XElement In xInfo...<display_location>

                        _displayLocation =
                            New DisplayLocation(displayInfo...<full>.Value,
                                                displayInfo...<city>.Value,
                                                displayInfo...<state>.Value,
                                                displayInfo...<state_name>.Value,
                                                displayInfo...<country>.Value,
                                                displayInfo...<country_iso3166>.Value,
                                                displayInfo...<zip>.Value,
                                                displayInfo...<latitude>.Value,
                                                displayInfo...<longitude>.Value,
                                                displayInfo...<elevation>.Value)
                    Next

                    ' Observation Location
                    For Each observInfo As XElement In xInfo...<observation_location>

                        _observationLocation = New _
                            ObservationLocation(observInfo...<full>.Value,
                                                observInfo...<city>.Value,
                                                observInfo...<state>.Value,
                                                observInfo...<country>.Value,
                                                observInfo...<country_iso3166>.Value,
                                                observInfo...<latitude>.Value,
                                                observInfo...<longitude>.Value,
                                                observInfo...<elevation>.Value)
                    Next

                    ' Observation Time
                    _observationTime = New ObservationTime(xInfo...<observation_time_rfc822>.Value,
                                                           xInfo...<local_tz_short>.Value,
                                                           xInfo...<local_tz_long>.Value)

                    ' Current Conditions
                    _currentWeather = xInfo...<weather>.Value

                    ' Temperature
                    _temp = New Temperature(xInfo...<temperature_string>.Value,
                                            xInfo...<temp_f>.Value,
                                            xInfo...<temp_c>.Value)

                    ' Relative Humidity
                    _rh = New RelativeHumidity(xInfo...<relative_humidity>.Value)

                    ' Wind
                    _wind = New Wind(xInfo...<wind_string>.Value,
                                     xInfo...<wind_dir>.Value,
                                     xInfo...<wind_degrees>.Value,
                                     xInfo...<wind_mph>.Value,
                                     xInfo...<wind_gust_mph>.Value,
                                     xInfo...<wind_kph>.Value,
                                     xInfo...<wind_gust_kph>.Value)

                    ' Pressure
                    _pressure = New Pressure(xInfo...<pressure_mb>.Value,
                                             xInfo...<pressure_in>.Value,
                                             xInfo...<pressure_trend>.Value)

                    ' DewPoint
                    _dp = New DewPoint(xInfo...<dewpoint_string>.Value,
                                       xInfo...<dewpoint_f>.Value,
                                       xInfo...<dewpoint_c>.Value)

                    ' Heat Index
                    _hi = New HeatIndex(xInfo...<heat_index_string>.Value,
                                        xInfo...<heat_index_f>.Value,
                                        xInfo...<heat_index_c>.Value)

                    ' Windchill
                    _wc = New WindChill(xInfo...<windchill_string>.Value,
                                        xInfo...<windchill_f>.Value,
                                        xInfo...<windchill_c>.Value)

                    ' Feels Like Temperature
                    _feelsLikeTemp = New FeelsLikeTemperature(xInfo...<feelslike_string>.Value,
                                                              xInfo...<feelslike_f>.Value,
                                                              xInfo...<feelslike_c>.Value)

                    ' Visibility
                    _visibility = New Visibility(xInfo...<visibility_mi>.Value,
                                                 xInfo...<visibility_km>.Value)

                    ' Precipitation
                    _precip = New Precipitation(xInfo...<precip_1hr_in>.Value,
                                                xInfo...<precip_1hr_metric>.Value,
                                                xInfo...<precip_today_in>.Value,
                                                xInfo...<precip_today_metric>.Value)

                    ' Weather Icon
                    _icon = New WeatherIcon(xInfo...<icon>.Value,
                                            xInfo...<icon_url>.Value)

                Next

                WU_Compliance.CleanUpCompliance()

            Catch ex As Exception
                _currentWeather = "--"
                _displayLocation = Nothing
                _observationLocation = Nothing
                _observationTime = Nothing
                _temp = Nothing
                _rh = Nothing
                _wind = Nothing
                _pressure = Nothing
                _dp = Nothing
                _hi = Nothing
                _wc = Nothing
                _feelsLikeTemp = Nothing
                _visibility = Nothing
                _precip = Nothing
                _icon = Nothing
            End Try

        End Sub

        ''' <summary>
        ''' The current conditions at this location shown as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentWeatherConditions() As String
            Get
                Return _currentWeather
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the location of these current conditions.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DisplayLocation() As DisplayLocation
            Get
                Return _displayLocation
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the location of the place of observation for these current conditions.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ObservationLocation() As ObservationLocation
            Get
                Return _observationLocation
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the time of the place of observation for these current conditions.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ObservationTime() As ObservationTime
            Get
                Return _observationTime
            End Get
        End Property

        ''' <summary>
        '''  An instance of a class which will show information about the current temperature for this location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Temperature() As Temperature
            Get
                Return _temp
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the current relative humidity for this location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property RelativeHumidity() As RelativeHumidity
            Get
                Return _rh
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the current wind conditions for this location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Wind() As Wind
            Get
                Return _wind
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the current barometric pressure for this location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Pressure() As Pressure
            Get
                Return _pressure
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the current dewpoint temperature for this location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DewPoint() As DewPoint
            Get
                Return _dp
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the current heat index temperature for this location (if applicable).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HeatIndex() As HeatIndex
            Get
                Return _hi
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the current windchill temperature for this location (if applicable).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindChill() As WindChill
            Get
                Return _wc
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the current "feels like" temperature for this location (if applicable).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FeelsLikeTemperature() As FeelsLikeTemperature
            Get
                Return _feelsLikeTemp
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the current visibility for this location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Visibility() As Visibility
            Get
                Return _visibility
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about the current precipitation for this location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Precipitation() As Precipitation
            Get
                Return _precip
            End Get
        End Property

        ''' <summary>
        ''' An instance of a class which will show information about icons (images) applicable to the current condition for this location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WeatherIcon() As WeatherIcon
            Get
                Return _icon
            End Get
        End Property
    End Class





    ''' <summary>
    ''' A class which exposes instances of various other classes which contain the specific data for the 48 hour forecast.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Forecast_48Hours
        Private _listOfForecastDataExtendedInfo As New List(Of ForecastData_ExtendedInfo)
        Private _listOfForecastDataText As New List(Of ForecastData_Text)

        ''' <summary>
        ''' Instantiates this class using your API key only. This will use your IP address to determine your location.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                GetForecast(apiKey, "AutoIP")
            End If

        End Sub

        ''' <summary>
        ''' this class using your API key and either your zip code/postal code or an airport code.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="zipCodeOrAirportCode">The zip code/postal code or an airport code.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal zipCodeOrAirportCode As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                GetForecast(apiKey, zipCodeOrAirportCode)
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key, your city's name and either your state's name or the country's name.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="cityName">The name of the city (not case-sensitive).</param>
        ''' <param name="stateNameOrCountryName">The name of the state or country (not case-sensitive).</param>
        Public Sub New(ByVal apiKey As String,
                       ByVal cityName As String,
                       ByVal stateNameOrCountryName As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                Dim sb As New System.Text.StringBuilder

                sb.Append(stateNameOrCountryName)
                sb.Append("/")
                sb.Append(cityName)

                GetForecast(apiKey, sb.ToString)
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key and a latitude/longitude pair. Note that the latitude and longitude are type Double, not type String!
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="latitude">The latitude (type Double).</param>
        ''' <param name="longitude">The longitude (type Double).</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal latitude As Double,
                       ByVal longitude As Double)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                Dim sb As New System.Text.StringBuilder

                sb.Append(latitude.ToString)
                sb.Append(",")
                sb.Append(longitude.ToString)

                GetForecast(apiKey, sb.ToString)
            End If

        End Sub

        Private Sub GetForecast(ByVal apiKey As String, ByVal locationQry As String)

            Try
                Dim sb As New System.Text.StringBuilder

                sb.Append("http://api.wunderground.com/api/")
                sb.Append(apiKey.Trim)
                sb.Append("/forecast/q/")
                sb.Append(locationQry.Trim)
                sb.Append(".xml")

                Dim _forecastPeriod As ForecastTime = Nothing
                Dim _forecastConditions As String = "--"
                Dim _forecastPOP As ProbabilityOfPrecipitation = Nothing
                Dim _forecastHighTemp As Temperature = Nothing
                Dim _forecastLowTemp As Temperature = Nothing
                Dim _forecastIcon As WeatherIcon = Nothing
                Dim _qpfAllDay As QuantitativePrecipitationForecast = Nothing
                Dim _qpfDay As QuantitativePrecipitationForecast = Nothing
                Dim _qpfNight As QuantitativePrecipitationForecast = Nothing
                Dim _snowAllDay As SnowForecast = Nothing
                Dim _snowDay As SnowForecast = Nothing
                Dim _snowNight As SnowForecast = Nothing
                Dim _maxWind As WindForecast = Nothing
                Dim _aveWind As WindForecast = Nothing
                Dim _aveHumidity As RelativeHumidity = Nothing
                Dim _maxHumidity As RelativeHumidity = Nothing
                Dim _minHumidity As RelativeHumidity = Nothing

                Dim _weatherIcon As WeatherIcon = Nothing
                Dim _title As String = "--"
                Dim _forecastText_Imperial As String = "--"
                Dim _forecastText_SI As String = "--"
                Dim _pop As ProbabilityOfPrecipitation = Nothing

                Dim xInfo = XElement.Load(sb.ToString)

                For Each ExtendedInfoInfo As XElement In xInfo...<simpleforecast>
                    For Each fInfo As XElement In ExtendedInfoInfo...<forecastdays>
                        For Each fDayInfo As XElement In fInfo...<forecastday>

                            ' Forecast Period
                            Dim forecastDate As New _
                                DateTime(CInt(fDayInfo...<year>.Value),
                                         CInt(fDayInfo...<month>.Value),
                                         CInt(fDayInfo...<day>.Value),
                                         CInt(fDayInfo...<hour>.Value),
                                         CInt(fDayInfo...<min>.Value),
                                         CInt(fDayInfo...<sec>.Value))

                            _forecastPeriod = New _
                                ForecastTime(forecastDate.ToString,
                                             fDayInfo...<tz_short>.Value,
                                             fDayInfo...<tz_long>.Value)

                            ' Forecast High Temp
                            For Each highInfo As XElement In fDayInfo...<high>
                                _forecastHighTemp = New Temperature(highInfo...<fahrenheit>.Value,
                                                                    highInfo...<fahrenheit>.Value,
                                                                    highInfo...<celsius>.Value)
                            Next

                            ' Forecast Low Temp
                            For Each lowInfo As XElement In fDayInfo...<low>
                                _forecastLowTemp = New Temperature(lowInfo...<fahrenheit>.Value,
                                                                   lowInfo...<fahrenheit>.Value,
                                                                   lowInfo...<celsius>.Value)
                            Next

                            ' Forecast Conditions
                            _forecastConditions = fDayInfo...<conditions>.Value

                            ' Weather Icon
                            _forecastIcon = New WeatherIcon(fDayInfo...<icon>.Value,
                                                            fDayInfo...<icon_url>.Value)

                            ' Probability Of Precipitation
                            _forecastPOP = New ProbabilityOfPrecipitation(fDayInfo...<pop>.Value)

                            ' QPF All Day
                            For Each qInfo As XElement In fDayInfo...<qpf_allday>
                                _qpfAllDay = New _
                                    QuantitativePrecipitationForecast(qInfo...<in>.Value,
                                                                      qInfo...<mm>.Value)

                            Next

                            ' QPF Day
                            For Each qInfo As XElement In fDayInfo...<qpf_day>
                                _qpfDay = New _
                                    QuantitativePrecipitationForecast(qInfo...<in>.Value,
                                                                      qInfo...<mm>.Value)
                            Next

                            ' QPF Night
                            For Each qInfo As XElement In fDayInfo...<qpf_night>
                                _qpfNight = New _
                                    QuantitativePrecipitationForecast(qInfo...<in>.Value,
                                                                      qInfo...<mm>.Value)
                            Next

                            ' Snow All Day
                            For Each snowInfo As XElement In fDayInfo...<snow_allday>
                                _snowAllDay = New _
                                    SnowForecast(snowInfo...<in>.Value,
                                                 snowInfo...<cm>.Value)
                            Next

                            ' Snow Day
                            For Each snowInfo As XElement In fDayInfo...<snow_day>
                                _snowDay = New _
                                    SnowForecast(snowInfo...<in>.Value,
                                                 snowInfo...<cm>.Value)
                            Next

                            ' Snow Night
                            For Each snowInfo As XElement In fDayInfo...<snow_night>
                                _snowNight = New _
                                    SnowForecast(snowInfo...<in>.Value,
                                                 snowInfo...<cm>.Value)
                            Next


                            ' Maximum Wind
                            For Each maxWindInfo As XElement In fDayInfo...<maxwind>
                                _maxWind = New WindForecast(maxWindInfo...<mph>.Value,
                                                            maxWindInfo...<kph>.Value,
                                                            maxWindInfo...<dir>.Value,
                                                            maxWindInfo...<degrees>.Value)
                            Next

                            ' Average Wind
                            For Each aveWindInfo As XElement In fDayInfo...<avewind>
                                _aveWind = New WindForecast(aveWindInfo...<mph>.Value,
                                                            aveWindInfo...<kph>.Value,
                                                            aveWindInfo...<dir>.Value,
                                                            aveWindInfo...<degrees>.Value)
                            Next

                            ' Average RH
                            _aveHumidity = New RelativeHumidity(fDayInfo...<avehumidity>.Value)

                            ' Maximum RH
                            _maxHumidity = New RelativeHumidity(fDayInfo...<maxhumidity>.Value)

                            ' Minimum RH
                            _minHumidity = New RelativeHumidity(fDayInfo...<minhumidity>.Value)

                            ' Add To List...
                            _listOfForecastDataExtendedInfo.Add(New _
                                                    ForecastData_ExtendedInfo(_forecastPeriod,
                                                                        _forecastConditions,
                                                                        _forecastPOP,
                                                                        _forecastHighTemp,
                                                                        _forecastLowTemp,
                                                                        _forecastIcon,
                                                                        _qpfAllDay,
                                                                        _qpfDay,
                                                                        _qpfNight,
                                                                        _snowAllDay,
                                                                        _snowDay,
                                                                        _snowNight,
                                                                        _maxWind,
                                                                        _aveWind,
                                                                        _aveHumidity,
                                                                        _maxHumidity,
                                                                        _minHumidity))
                        Next
                    Next
                Next

                For Each txtInfo As XElement In xInfo...<txt_forecast>
                    For Each forecastDaysInfo As XElement In txtInfo...<forecastdays>
                        For Each fDayInfo As XElement In forecastDaysInfo...<forecastday>

                            ' Weather Icon
                            _weatherIcon = New _
                                WeatherIcon(fDayInfo...<icon>.Value,
                                            fDayInfo...<icon_url>.Value)

                            ' Title
                            _title = fDayInfo...<title>.Value

                            ' Forecast (Imperial)
                            _forecastText_Imperial = fDayInfo...<fcttext>.Value

                            ' Forecast (Metric)
                            _forecastText_SI = fDayInfo...<fcttext_metric>.Value

                            ' Probability Of Precipitation
                            _pop = New ProbabilityOfPrecipitation(fDayInfo...<pop>.Value)

                            ' Add To List...
                            _listOfForecastDataText.Add(New ForecastData_Text(_weatherIcon,
                                                                          _title,
                                                                          _forecastText_Imperial,
                                                                          _forecastText_SI,
                                                                          _pop))
                        Next
                    Next
                Next

                WU_Compliance.CleanUpCompliance()

            Catch ex As Exception
                _listOfForecastDataExtendedInfo = Nothing
                _listOfForecastDataText = Nothing
            End Try

        End Sub

        ''' <summary>
        ''' A list of 48 hour forecast extended information.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ListOfForecast_48Hours_ExtendedInfo() As List(Of ForecastData_ExtendedInfo)
            Get
                Return _listOfForecastDataExtendedInfo
            End Get
        End Property

        ''' <summary>
        ''' A list of 48 hour forecast text information.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ListOfForecast_48Hours_Text() As List(Of ForecastData_Text)
            Get
                Return _listOfForecastDataText
            End Get
        End Property
    End Class





    ''' <summary>
    ''' A class which exposes instances of various other classes which contain the specific data for the 10 day forecast.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Forecast_10Day
        Private _listOfForecastData_Extended As New List(Of ForecastData_ExtendedInfo)
        Private _listOfForecastData_Text As New List(Of ForecastData_Text)

        ''' <summary>
        ''' Instantiates this class using your API key only. This will use your IP address to determine your location.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                GetForecast(apiKey, "AutoIP")
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key and either your zip code/postal code or an airport code.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="zipCodeOrAirportCode">The zip code/postal code or an airport code.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal zipCodeOrAirportCode As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                GetForecast(apiKey, zipCodeOrAirportCode)
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key, your city's name and either your state's name or the country's name.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="cityName">The name of the city (not case-sensitive).</param>
        ''' <param name="stateNameOrCountryName">The name of the state or country (not case-sensitive).</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal cityName As String,
                       ByVal stateNameOrCountryName As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                Dim sb As New System.Text.StringBuilder

                sb.Append(stateNameOrCountryName)
                sb.Append("/")
                sb.Append(cityName)

                GetForecast(apiKey, sb.ToString)
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key and a latitude/longitude pair. Note that the latitude and longitude are type Double, not type String!
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="latitude">The latitude (type Double).</param>
        ''' <param name="longitude">The longitude (type Double).</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal latitude As Double,
                       ByVal longitude As Double)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                Dim sb As New System.Text.StringBuilder

                sb.Append(latitude.ToString)
                sb.Append(",")
                sb.Append(longitude.ToString)

                GetForecast(apiKey, sb.ToString)
            End If

        End Sub

        Private Sub GetForecast(ByVal apiKey As String, ByVal locationQry As String)

            Try
                Dim sb As New System.Text.StringBuilder

                sb.Append("http://api.wunderground.com/api/")
                sb.Append(apiKey.Trim)
                sb.Append("/forecast10day/q/")
                sb.Append(locationQry.Trim)
                sb.Append(".xml")

                Dim _forecastPeriod As ForecastTime = Nothing
                Dim _forecastConditions As String = "--"
                Dim _forecastPOP As ProbabilityOfPrecipitation = Nothing
                Dim _forecastHighTemp As Temperature = Nothing
                Dim _forecastLowTemp As Temperature = Nothing
                Dim _forecastIcon As WeatherIcon = Nothing
                Dim _qpfAllDay As QuantitativePrecipitationForecast = Nothing
                Dim _qpfDay As QuantitativePrecipitationForecast = Nothing
                Dim _qpfNight As QuantitativePrecipitationForecast = Nothing
                Dim _snowAllDay As SnowForecast = Nothing
                Dim _snowDay As SnowForecast = Nothing
                Dim _snowNight As SnowForecast = Nothing
                Dim _maxWind As WindForecast = Nothing
                Dim _aveWind As WindForecast = Nothing
                Dim _aveHumidity As RelativeHumidity = Nothing
                Dim _maxHumidity As RelativeHumidity = Nothing
                Dim _minHumidity As RelativeHumidity = Nothing

                Dim _weatherIcon As WeatherIcon = Nothing
                Dim _title As String = "--"
                Dim _forecastText_Imperial As String = "--"
                Dim _forecastText_SI As String = "--"
                Dim _pop As ProbabilityOfPrecipitation = Nothing

                Dim xInfo = XElement.Load(sb.ToString)

                For Each simpleInfo As XElement In xInfo...<simpleforecast>
                    For Each fInfo As XElement In simpleInfo...<forecastdays>
                        For Each fDayInfo As XElement In fInfo...<forecastday>

                            ' Forecast Period
                            Dim forecastDate As New _
                                DateTime(CInt(fDayInfo...<year>.Value),
                                         CInt(fDayInfo...<month>.Value),
                                         CInt(fDayInfo...<day>.Value),
                                         CInt(fDayInfo...<hour>.Value),
                                         CInt(fDayInfo...<min>.Value),
                                         CInt(fDayInfo...<sec>.Value))

                            _forecastPeriod = New _
                                ForecastTime(forecastDate.ToString,
                                             fDayInfo...<tz_short>.Value,
                                             fDayInfo...<tz_long>.Value)

                            ' Forecast High Temp
                            For Each highInfo As XElement In fDayInfo...<high>
                                _forecastHighTemp = New Temperature(highInfo...<fahrenheit>.Value,
                                                                    highInfo...<fahrenheit>.Value,
                                                                    highInfo...<celsius>.Value)
                            Next

                            ' Forecast Low Temp
                            For Each lowInfo As XElement In fDayInfo...<low>
                                _forecastLowTemp = New Temperature(lowInfo...<fahrenheit>.Value,
                                                                   lowInfo...<fahrenheit>.Value,
                                                                   lowInfo...<celsius>.Value)
                            Next

                            ' Forecast Conditions
                            _forecastConditions = fDayInfo...<conditions>.Value

                            ' Weather Icon
                            _forecastIcon = New WeatherIcon(fDayInfo...<icon>.Value,
                                                            fDayInfo...<icon_url>.Value)

                            ' Probability Of Precipitation
                            _forecastPOP = New ProbabilityOfPrecipitation(fDayInfo...<pop>.Value)

                            ' QPF All Day
                            For Each qInfo As XElement In fDayInfo...<qpf_allday>
                                _qpfAllDay = New _
                                    QuantitativePrecipitationForecast(qInfo...<in>.Value,
                                                                      qInfo...<mm>.Value)

                            Next

                            ' QPF Day
                            For Each qInfo As XElement In fDayInfo...<qpf_day>
                                _qpfDay = New _
                                    QuantitativePrecipitationForecast(qInfo...<in>.Value,
                                                                      qInfo...<mm>.Value)
                            Next

                            ' QPF Night
                            For Each qInfo As XElement In fDayInfo...<qpf_night>
                                _qpfNight = New _
                                    QuantitativePrecipitationForecast(qInfo...<in>.Value,
                                                                      qInfo...<mm>.Value)
                            Next

                            ' Snow All Day
                            For Each snowInfo As XElement In fDayInfo...<snow_allday>
                                _snowAllDay = New _
                                    SnowForecast(snowInfo...<in>.Value,
                                                 snowInfo...<cm>.Value)
                            Next

                            ' Snow Day
                            For Each snowInfo As XElement In fDayInfo...<snow_day>
                                _snowDay = New _
                                    SnowForecast(snowInfo...<in>.Value,
                                                 snowInfo...<cm>.Value)
                            Next

                            ' Snow Night
                            For Each snowInfo As XElement In fDayInfo...<snow_night>
                                _snowNight = New _
                                    SnowForecast(snowInfo...<in>.Value,
                                                 snowInfo...<cm>.Value)
                            Next


                            ' Maximum Wind
                            For Each maxWindInfo As XElement In fDayInfo...<maxwind>
                                _maxWind = New WindForecast(maxWindInfo...<mph>.Value,
                                                            maxWindInfo...<kph>.Value,
                                                            maxWindInfo...<dir>.Value,
                                                            maxWindInfo...<degrees>.Value)
                            Next

                            ' Average Wind
                            For Each aveWindInfo As XElement In fDayInfo...<avewind>
                                _aveWind = New WindForecast(aveWindInfo...<mph>.Value,
                                                            aveWindInfo...<kph>.Value,
                                                            aveWindInfo...<dir>.Value,
                                                            aveWindInfo...<degrees>.Value)
                            Next

                            ' Average RH
                            _aveHumidity = New RelativeHumidity(fDayInfo...<avehumidity>.Value)

                            ' Maximum RH
                            _maxHumidity = New RelativeHumidity(fDayInfo...<maxhumidity>.Value)

                            ' Minimum RH
                            _minHumidity = New RelativeHumidity(fDayInfo...<minhumidity>.Value)

                            ' Add To List...
                            _listOfForecastData_Extended.Add(New _
                                                    ForecastData_ExtendedInfo(_forecastPeriod,
                                                                              _forecastConditions,
                                                                              _forecastPOP,
                                                                              _forecastHighTemp,
                                                                              _forecastLowTemp,
                                                                              _forecastIcon,
                                                                              _qpfAllDay,
                                                                              _qpfDay,
                                                                              _qpfNight,
                                                                              _snowAllDay,
                                                                              _snowDay,
                                                                              _snowNight,
                                                                              _maxWind,
                                                                              _aveWind,
                                                                              _aveHumidity,
                                                                              _maxHumidity,
                                                                              _minHumidity))
                        Next
                    Next
                Next

                For Each txtInfo As XElement In xInfo...<txt_forecast>
                    For Each forecastDaysInfo As XElement In txtInfo...<forecastdays>
                        For Each fDayInfo As XElement In forecastDaysInfo...<forecastday>

                            ' Weather Icon
                            _weatherIcon = New _
                                WeatherIcon(fDayInfo...<icon>.Value,
                                            fDayInfo...<icon_url>.Value)

                            ' Title
                            _title = fDayInfo...<title>.Value

                            ' Forecast (Imperial)
                            _forecastText_Imperial = fDayInfo...<fcttext>.Value

                            ' Forecast (Metric)
                            _forecastText_SI = fDayInfo...<fcttext_metric>.Value

                            ' Probability Of Precipitation
                            _pop = New ProbabilityOfPrecipitation(fDayInfo...<pop>.Value)

                            ' Add To List...
                            _listOfForecastData_Text.Add(New ForecastData_Text(_weatherIcon,
                                                                          _title,
                                                                          _forecastText_Imperial,
                                                                          _forecastText_SI,
                                                                          _pop))
                        Next
                    Next
                Next

                WU_Compliance.CleanUpCompliance()

            Catch ex As Exception
                _listOfForecastData_Extended = Nothing
                _listOfForecastData_Text = Nothing
            End Try

        End Sub

        ''' <summary>
        ''' A list of 10 day forecast extended information.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ListOfForecast_10Day_ExtendedInfo() As List(Of ForecastData_ExtendedInfo)
            Get
                Return _listOfForecastData_Extended
            End Get
        End Property

        ''' <summary>
        ''' A list of 10 day forecast text information.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ListOfForecast_10Day_Text() As List(Of ForecastData_Text)
            Get
                Return _listOfForecastData_Text
            End Get
        End Property
    End Class




    ''' <summary>
    ''' A class which exposes instances of various other classes which contain the specific data for the astronomical information.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Astronomy
        Private _sun As Sun
        Private _moon As Moon

        ''' <summary>
        ''' Instantiates this class using your API key only. This will use your IP address to determine your location.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                GetAstronomy(apiKey, "AutoIP")
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key and either your zip code/postal code or an airport code.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="zipCodeOrAirportCode">The zip code/postal code or an airport code.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal zipCodeOrAirportCode As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                GetAstronomy(apiKey, zipCodeOrAirportCode)
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key, your city's name and either your state's name or the country's name.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="cityName">The name of the city (not case-sensitive).</param>
        ''' <param name="stateNameOrCountryName">The name of the state or country (not case-sensitive).</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal cityName As String,
                       ByVal stateNameOrCountryName As String)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                Dim sb As New System.Text.StringBuilder

                sb.Append(stateNameOrCountryName)
                sb.Append("/")
                sb.Append(cityName)

                GetAstronomy(apiKey, sb.ToString)
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key and a latitude/longitude pair. Note that the latitude and longitude are type Double, not type String!
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="latitude">The latitude (type Double).</param>
        ''' <param name="longitude">The longitude (type Double).</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal latitude As Double,
                       ByVal longitude As Double)

            Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

            If countRemainingForToday = 0 Then
                Exit Sub
            Else
                Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                Dim msToWait As Integer = CInt(secsToWait * 1000)

                If msToWait > 0 Then
                    Threading.Thread.Sleep(msToWait)
                End If

                Dim sb As New System.Text.StringBuilder

                sb.Append(latitude.ToString)
                sb.Append(",")
                sb.Append(longitude.ToString)

                GetAstronomy(apiKey, sb.ToString)
            End If

        End Sub

        Private Sub GetAstronomy(ByVal apiKey As String, ByVal locationQry As String)

            Try
                Dim sb As New System.Text.StringBuilder

                sb.Append("http://api.wunderground.com/api/")
                sb.Append(apiKey.Trim)
                sb.Append("/astronomy/q/")
                sb.Append(locationQry.Trim)
                sb.Append(".xml")

                Dim xInfo = XElement.Load(sb.ToString)

                ' Moon
                For Each moonInfo As XElement In xInfo...<moon_phase>
                    _moon = New Moon(moonInfo...<percentIlluminated>.Value,
                                     moonInfo...<ageOfMoon>.Value)
                Next

                ' Sun
                For Each sunInfo As XElement In xInfo...<sun_phase>
                    Dim sunsetHour As String = ""
                    Dim sunsetMinute As String = ""
                    Dim sunriseHour As String = ""
                    Dim sunriseMinute As String = ""

                    ' Sunset
                    For Each sunsetInfo As XElement In sunInfo...<sunset>
                        sunsetHour = sunsetInfo...<hour>.Value
                        sunsetMinute = sunsetInfo...<minute>.Value
                    Next

                    ' Sunrise
                    For Each sunriseInfo As XElement In sunInfo...<sunrise>
                        sunriseHour = sunriseInfo...<hour>.Value
                        sunriseMinute = sunriseInfo...<minute>.Value
                    Next

                    _sun = New Sun(sunriseHour, sunriseMinute, sunsetHour, sunsetMinute)
                Next

                WU_Compliance.CleanUpCompliance()

            Catch ex As Exception
                _sun = Nothing
                _moon = Nothing
            End Try

        End Sub

        ''' <summary>
        ''' Information about the moon's current phase.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Moon() As Moon
            Get
                Return _moon
            End Get
        End Property

        ''' <summary>
        ''' The location's sunrise and sunset times.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Sun() As Sun
            Get
                Return _sun
            End Get
        End Property
    End Class





    ''' <summary>
    ''' A class which exposes a read-only property for a bitmap of the satellite image for the specified location.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RadarOrSatelliteImage
        ''' <summary>
        ''' An enumerator of the types available for return.
        ''' </summary>
        ''' <remarks></remarks>
        Public Enum ImageOf
            ''' <summary>
            ''' This will return the radar image for the location selected.
            ''' </summary>
            ''' <remarks></remarks>
            Radar

            ''' <summary>
            ''' This will return the satellite image for the location selected.
            ''' </summary>
            ''' <remarks></remarks>
            Satellite
        End Enum

        Private _image As Bitmap

        ''' <summary>
        ''' Instantiates this class using your API key only. This will use your IP address to determine your location.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="returnType">Specify what you want returned (either radar or satellite).</param>
        ''' <param name="imageWidth">The width of the image in pixels (50 min., 1024 max.).</param>
        ''' <param name="imageHeight">The height of the image in pixels (50 min., 1024 max.).</param>
        ''' <param name="zoomLevel">The zoom level to use (1 min., 2000 max.). Note: Use 100 for most uses.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal returnType As ImageOf,
                       ByVal imageWidth As Integer,
                       ByVal imageHeight As Integer,
                       ByVal zoomLevel As Integer)

            If imageWidth < 50 Then
                Throw New Exception("The image width must be at least 50 pixels.")

            ElseIf imageWidth > 1024 Then
                Throw New Exception("The image width can be no greater than 1024 pixels.")

            ElseIf imageHeight < 50 Then
                Throw New Exception("The image height must be at least 50 pixels.")

            ElseIf imageHeight > 1024 Then
                Throw New Exception("The image height can be no greater than 1024 pixels.")

            ElseIf zoomLevel < 1 Then
                Throw New Exception("The zoom level must be at least 1.")

            ElseIf zoomLevel > 2000 Then
                Throw New Exception("The zoom level can be no greater than 2000.")

            Else
                Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

                If countRemainingForToday = 0 Then
                    Exit Sub
                Else
                    Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                    Dim msToWait As Integer = CInt(secsToWait * 1000)

                    If msToWait > 0 Then
                        Threading.Thread.Sleep(msToWait)
                    End If

                    GetImage(apiKey, returnType, "AutoIP", imageWidth, imageHeight, zoomLevel)
                End If
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key and either your zip code/postal code or an airport code.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="returnType">Specify what you want returned (either radar or satellite).</param>
        ''' <param name="zipCodeOrAirportCode">The zip code/postal code or an airport code.</param>
        ''' <param name="imageWidth">The width of the image in pixels (50 min., 1024 max.).</param>
        ''' <param name="imageHeight">The height of the image in pixels (50 min., 1024 max.).</param>
        ''' <param name="zoomLevel">The zoom level to use (1 min., 2000 max.). Note: Use 100 for most uses.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal returnType As ImageOf,
                       ByVal zipCodeOrAirportCode As String,
                       ByVal imageWidth As Integer,
                       ByVal imageHeight As Integer,
                       ByVal zoomLevel As Integer)

            If imageWidth < 50 Then
                Throw New Exception("The image width must be at least 50 pixels.")

            ElseIf imageWidth > 1024 Then
                Throw New Exception("The image width can be no greater than 1024 pixels.")

            ElseIf imageHeight < 50 Then
                Throw New Exception("The image height must be at least 50 pixels.")

            ElseIf imageHeight > 1024 Then
                Throw New Exception("The image height can be no greater than 1024 pixels.")
            ElseIf zoomLevel < 1 Then
                Throw New Exception("The zoom level must be at least 1.")

            ElseIf zoomLevel > 2000 Then
                Throw New Exception("The zoom level can be no greater than 2000.")

            Else
                Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

                If countRemainingForToday = 0 Then
                    Exit Sub
                Else
                    Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                    Dim msToWait As Integer = CInt(secsToWait * 1000)

                    If msToWait > 0 Then
                        Threading.Thread.Sleep(msToWait)
                    End If

                    GetImage(apiKey, returnType, zipCodeOrAirportCode, imageWidth, imageHeight, zoomLevel)
                End If
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key, your city's name and either your state's name or the country's name.
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="returnType">Specify what you want returned (either radar or satellite).</param>
        ''' <param name="cityName">The name of the city (not case-sensitive).</param>
        ''' <param name="stateNameOrCountryName">The name of the state or country (not case-sensitive).</param>
        ''' <param name="imageWidth">The width of the image in pixels (50 min., 1024 max.).</param>
        ''' <param name="imageHeight">The height of the image in pixels (50 min., 1024 max.).</param>
        ''' <param name="zoomLevel">The zoom level to use (1 min., 2000 max.). Note: Use 100 for most uses.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal returnType As ImageOf,
                       ByVal cityName As String,
                       ByVal stateNameOrCountryName As String,
                       ByVal imageWidth As Integer,
                       ByVal imageHeight As Integer,
                       ByVal zoomLevel As Integer)

            If imageWidth < 50 Then
                Throw New Exception("The image width must be at least 50 pixels.")

            ElseIf imageWidth > 1024 Then
                Throw New Exception("The image width can be no greater than 1024 pixels.")

            ElseIf imageHeight < 50 Then
                Throw New Exception("The image height must be at least 50 pixels.")

            ElseIf imageHeight > 1024 Then
                Throw New Exception("The image height can be no greater than 1024 pixels.")
            ElseIf zoomLevel < 1 Then
                Throw New Exception("The zoom level must be at least 1.")

            ElseIf zoomLevel > 2000 Then
                Throw New Exception("The zoom level can be no greater than 2000.")

            Else
                Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

                If countRemainingForToday = 0 Then
                    Exit Sub
                Else
                    Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                    Dim msToWait As Integer = CInt(secsToWait * 1000)

                    If msToWait > 0 Then
                        Threading.Thread.Sleep(msToWait)
                    End If

                    Dim sb As New System.Text.StringBuilder

                    sb.Append(stateNameOrCountryName)
                    sb.Append("/")
                    sb.Append(cityName)

                    GetImage(apiKey, returnType, sb.ToString, imageWidth, imageHeight, zoomLevel)
                End If
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class using your API key and a latitude/longitude pair. Note that the latitude and longitude are type Double, not type String!
        ''' </summary>
        ''' <param name="apiKey">Your Weather Underground free API key. If you do not yet have one, go to http://www.wunderground.com/weather/api/ and sign up for one. It's quite easy.</param>
        ''' <param name="returnType">Specify what you want returned (either radar or satellite).</param>
        ''' <param name="latitude">The latitude (type Double).</param>
        ''' <param name="longitude">The longitude (type Double).</param>
        ''' <param name="imageWidth">The width of the image in pixels (50 min., 1024 max.).</param>
        ''' <param name="imageHeight">The height of the image in pixels (50 min., 1024 max.).</param>
        ''' <param name="zoomLevel">The zoom level to use (1 min., 2000 max.). Note: Use 100 for most uses.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal apiKey As String,
                       ByVal returnType As ImageOf,
                       ByVal latitude As Double,
                       ByVal longitude As Double,
                       ByVal imageWidth As Integer,
                       ByVal imageHeight As Integer,
                       ByVal zoomLevel As Integer)

            If imageWidth < 50 Then
                Throw New Exception("The image width must be at least 50 pixels.")

            ElseIf imageWidth > 1024 Then
                Throw New Exception("The image width can be no greater than 1024 pixels.")

            ElseIf imageHeight < 50 Then
                Throw New Exception("The image height must be at least 50 pixels.")

            ElseIf imageHeight > 1024 Then
                Throw New Exception("The image height can be no greater than 1024 pixels.")

            ElseIf zoomLevel < 1 Then
                Throw New Exception("The zoom level must be at least 1.")

            ElseIf zoomLevel > 2000 Then
                Throw New Exception("The zoom level can be no greater than 2000.")

            Else
                Dim countRemainingForToday As Integer = WU_Compliance.ReturnCallsToday

                If countRemainingForToday = 0 Then
                    Exit Sub
                Else
                    Dim secsToWait As Double = WU_Compliance.ReturnSecsToWait
                    Dim msToWait As Integer = CInt(secsToWait * 1000)

                    If msToWait > 0 Then
                        Threading.Thread.Sleep(msToWait)
                    End If

                    Dim sb As New System.Text.StringBuilder

                    sb.Append(latitude.ToString)
                    sb.Append(",")
                    sb.Append(longitude.ToString)

                    GetImage(apiKey, returnType, sb.ToString, imageWidth, imageHeight, zoomLevel)
                End If
            End If

        End Sub

        ''' <summary>
        ''' Instantiates this class without parameters. Note that until one of the other constructors is used, all properties will show with a value of "Nothing".
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
        End Sub

        Private Sub GetImage(ByVal apiKey As String,
                             ByVal imgType As ImageOf,
                             ByVal locationQry As String,
                             ByVal width As Integer,
                             ByVal height As Integer,
                             ByVal zoom As Integer)

            Try
                Dim sb As New System.Text.StringBuilder

                sb.Append("http://api.wunderground.com/api/")
                sb.Append(apiKey.Trim)

                If imgType = ImageOf.Radar Then
                    sb.Append("/radar/q/")

                ElseIf imgType = ImageOf.Satellite Then
                    sb.Append("/satellite/q/")
                End If

                sb.Append(locationQry.Trim)
                sb.Append(".png?width=")
                sb.Append(width.ToString)
                sb.Append("&height=")
                sb.Append(height.ToString)
                sb.Append("&newmaps=1")
                sb.Append("&radius=")
                sb.Append(zoom.ToString)

                Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(sb.ToString)

                Using request As System.Net.WebResponse = req.GetResponse
                    Using stream As System.IO.Stream = request.GetResponseStream
                        _image = New Bitmap(System.Drawing.Image.FromStream(stream))
                    End Using
                End Using

            Catch ex As Exception
                _image = Nothing
            End Try

        End Sub

        ''' <summary>
        ''' The image (radar or satellite, depending on what you instructed it to return) for the location selected.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Image() As Bitmap
            Get
                Return _image
            End Get
        End Property

    End Class



#End Region



#Region "Helper Classes"

    Public Class WU_Compliance
        Private Const complianceMinSecondsBetweenCalls As Double = 6.25
        Private Const complianceMaxPerDay As Integer = 500

        Public Shared Function ReturnSecsToWait() As Double
            Dim retVal As Double = 0

            Try
                Dim programDataFolderPath As String =
                               IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                               My.Application.Info.AssemblyName)

                If Not My.Computer.FileSystem.DirectoryExists(programDataFolderPath) Then
                    My.Computer.FileSystem.CreateDirectory(programDataFolderPath)
                End If

                Dim nyTimeFilePath As String = IO.Path.Combine(programDataFolderPath, "Compliance.txt")

                Dim easternTimeZone As TimeZoneInfo =
                    TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")

                Dim localTimeUTC As DateTime = TimeZoneInfo.ConvertTimeToUtc(Now)
                Dim nyTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(localTimeUTC, easternTimeZone)

                If My.Computer.FileSystem.FileExists(nyTimeFilePath) Then
                    Dim lastTime As DateTime

                    Using rdr As New System.IO.StreamReader(nyTimeFilePath)
                        Do While rdr.Peek() >= 0
                            Dim itm As String = rdr.ReadLine.Trim

                            If itm <> "" Then
                                lastTime = DateTime.Parse(itm)
                            End If
                        Loop
                    End Using

                    Dim differenceTS As TimeSpan = nyTime - lastTime
                    retVal = complianceMinSecondsBetweenCalls - differenceTS.TotalSeconds

                    If retVal < 0 Then
                        retVal = 0
                    End If
                Else
                    retVal = 6
                End If

                My.Computer.FileSystem.WriteAllText(nyTimeFilePath,
                            nyTime.AddSeconds(retVal).ToString & vbCrLf, True)

            Catch ex As Exception
                retVal = 6
            End Try

            Return retVal

        End Function

        Public Shared Function ReturnCallsToday() As Integer
            Dim retVal As Integer = 0

            Try
                Dim programDataFolderPath As String =
                    IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                    My.Application.Info.AssemblyName)

                Dim nyTimeFilePath As String = IO.Path.Combine(programDataFolderPath, "Compliance.txt")

                Dim easternTimeZone As TimeZoneInfo =
                   TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")

                Dim timeUtc As DateTime = DateTime.UtcNow
                Dim nyTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternTimeZone)

                Dim dateNY_Today As DateTime = nyTime.Date

                If My.Computer.FileSystem.FileExists(nyTimeFilePath) Then
                    Dim datesList As New List(Of DateTime)

                    Using rdr As New System.IO.StreamReader(nyTimeFilePath)
                        Do While rdr.Peek() >= 0
                            Dim itm As String = rdr.ReadLine.Trim

                            If itm <> "" Then
                                Dim testTime As DateTime = Now

                                If DateTime.TryParse(itm, testTime) Then
                                    datesList.Add(testTime)
                                End If
                            End If
                        Loop
                    End Using

                    Dim qry = From d As DateTime In datesList Where d.Date = dateNY_Today
                    retVal = complianceMaxPerDay - qry.Count
                Else
                    retVal = complianceMaxPerDay
                End If

            Catch ex As Exception
                retVal = 0
            End Try

            Return retVal

        End Function

        Public Shared Sub CleanUpCompliance()

            Try
                Dim programDataFolderPath As String =
                                 IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                                 My.Application.Info.AssemblyName)

                Dim nyTimeFilePath As String = IO.Path.Combine(programDataFolderPath, "Compliance.txt")

                Dim easternTimeZone As TimeZoneInfo =
                   TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")

                Dim timeUtc As DateTime = DateTime.UtcNow
                Dim nyTime As DateTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternTimeZone)

                Dim dateNY_Today As DateTime = nyTime.Date

                If My.Computer.FileSystem.FileExists(nyTimeFilePath) Then
                    Dim datesList As New List(Of DateTime)

                    Using rdr As New System.IO.StreamReader(nyTimeFilePath)
                        Do While rdr.Peek() >= 0
                            Dim itm As String = rdr.ReadLine.Trim

                            If itm <> "" Then
                                Dim testTime As DateTime = Now

                                If DateTime.TryParse(itm, testTime) Then
                                    datesList.Add(testTime)
                                End If
                            End If
                        Loop
                    End Using

                    Dim qry = From d As DateTime In datesList Where d.Date = dateNY_Today

                    If qry.Count > 0 Then
                        Dim sb As New System.Text.StringBuilder

                        For Each d As DateTime In qry
                            sb.AppendLine(d.ToString)
                        Next

                        My.Computer.FileSystem.WriteAllText(nyTimeFilePath, sb.ToString, False)
                    End If
                End If

            Catch ex As Exception
                ' Nothing we can do about it ...
            End Try

        End Sub

    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DisplayLocation
        Private _full As String = "--"
        Private _city As String = "--"
        Private _stateInitials As String = "--"
        Private _stateName As String = "--"
        Private _countryInitials As String = "--"
        Private _country_iso3166 As String = "--"
        Private _zip As String = "--"
        Private _latitude As Double
        Private _longitude As Double
        Private _elevation As Integer
        Private _latitudeString As String = "--"
        Private _longitudeString As String = "--"
        Private _elevationString As String = "--"

        Public Sub New(ByVal fullLocation As String,
                       ByVal city As String,
                       ByVal stateInitials As String,
                       ByVal stateName As String,
                       ByVal country As String,
                       ByVal countryISO3166 As String,
                       ByVal zipCode As String,
                       ByVal latitudeString As String,
                       ByVal longitudeString As String,
                       ByVal elevationString As String)

            _full = fullLocation
            _city = city
            _stateInitials = stateInitials
            _stateName = stateName
            _countryInitials = country
            _country_iso3166 = countryISO3166
            _zip = zipCode
            _latitudeString = latitudeString
            _longitudeString = longitudeString
            _elevationString = elevationString

            If _elevationString.Contains("."c) Then
                _elevationString = _elevationString.Substring(0, _elevationString.IndexOf("."c))
            End If

            If Not Double.TryParse(_latitudeString, _latitude) Then
                _latitude = -1000000
                _latitudeString = "N/A"
            Else
                _latitudeString = _latitude.ToString("f6")
            End If

            If Not Double.TryParse(_longitudeString, _longitude) Then
                _longitude = -1000000
                _longitudeString = "N/A"
            Else
                _longitudeString = _longitude.ToString("f6")
            End If

            If Not Integer.TryParse(_elevationString, _elevation) Then
                _elevation = -1000000
                _elevationString = "N/A"
            Else
                _elevationString = _elevation.ToString("f0") & " feet above sea level"
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The display location's full location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FullLocation() As String
            Get
                Return _full
            End Get
        End Property

        ''' <summary>
        ''' The display location's city name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property City() As String
            Get
                Return _city
            End Get
        End Property

        ''' <summary>
        ''' The display location's state initials.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property StateInitials() As String
            Get
                Return _stateInitials
            End Get
        End Property

        ''' <summary>
        ''' The display location's state full name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property StateName() As String
            Get
                Return _stateName
            End Get
        End Property

        ''' <summary>
        ''' The display location's country name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Country() As String
            Get
                Return _countryInitials
            End Get
        End Property

        ''' <summary>
        ''' The display location's country code per ISO3166.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CountryISO3166() As String
            Get
                Return _country_iso3166
            End Get
        End Property

        ''' <summary>
        ''' The display location's zip code / postal code (if applicable).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ZipCode() As String
            Get
                Return _zip
            End Get
        End Property

        ''' <summary>
        ''' The display location's latitude (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Latitude() As Double
            Get
                Return _latitude
            End Get
        End Property

        ''' <summary>
        ''' The display location's latitude as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property LatitudeString() As String
            Get
                Return _latitudeString
            End Get
        End Property

        ''' <summary>
        ''' The display location's longitude (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Longitude() As Double
            Get
                Return _longitude
            End Get
        End Property

        ''' <summary>
        ''' The display location's longitude as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property LongitudeString() As String
            Get
                Return _latitudeString
            End Get
        End Property

        ''' <summary>
        ''' The display location's elevation (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Elevation() As Integer
            Get
                Return _elevation
            End Get
        End Property

        ''' <summary>
        ''' The display location's elevation as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ElevationString() As String
            Get
                Return _elevationString
            End Get
        End Property

    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are. 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ObservationLocation
        Private _full As String = "--"
        Private _city As String = "--"
        Private _state As String = "--"
        Private _countryInitials As String = "--"
        Private _country_iso3166 As String = "--"
        Private _latitude As Double
        Private _longitude As Double
        Private _elevation As Integer
        Private _latitudeString As String = "--"
        Private _longitudeString As String = "--"
        Private _elevationString As String = "--"

        Public Sub New(ByVal fullLocation As String,
                       ByVal city As String,
                       ByVal stateName As String,
                       ByVal country As String,
                       ByVal countryISO3166 As String,
                       ByVal latitudeString As String,
                       ByVal longitudeString As String,
                       ByVal elevationString As String)

            _full = fullLocation
            _city = city
            _state = stateName
            _countryInitials = country
            _country_iso3166 = countryISO3166
            _latitudeString = latitudeString
            _longitudeString = longitudeString
            _elevationString = elevationString

            If _elevationString.Contains("."c) Then
                _elevationString = _elevationString.Substring(0, _elevationString.IndexOf("."c))
            End If

            If _elevationString.Contains(" ") Then
                _elevationString = _elevationString.Substring(0, _elevationString.IndexOf(" "))
            End If

            If Not Double.TryParse(_latitudeString, _latitude) Then
                _latitude = -1000000
                _latitudeString = "N/A"
            Else
                _latitudeString = _latitude.ToString("f6")
            End If

            If Not Double.TryParse(_longitudeString, _longitude) Then
                _longitude = -1000000
                _longitudeString = "N/A"
            Else
                _longitudeString = _longitude.ToString("f6")
            End If

            If Not Integer.TryParse(_elevationString, _elevation) Then
                _elevation = -1000000
                _elevationString = "N/A"
            Else
                _elevationString = _elevation.ToString("f0") & " feet above sea level"
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The observation location's full location.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FullLocation() As String
            Get
                Return _full
            End Get
        End Property

        ''' <summary>
        ''' The observation location's city name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property City() As String
            Get
                Return _city
            End Get
        End Property

        ''' <summary>
        ''' The observation location's state full name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property State() As String
            Get
                Return _state
            End Get
        End Property

        ''' <summary>
        ''' The observation location's country name's initials.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CountryInitials() As String
            Get
                Return _countryInitials
            End Get
        End Property

        ''' <summary>
        ''' The observation location's country code per ISO3166.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CountryISO3166() As String
            Get
                Return _country_iso3166
            End Get
        End Property

        ''' <summary>
        ''' The observation location's latitude (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Latitude() As Double
            Get
                Return _latitude
            End Get
        End Property

        ''' <summary>
        ''' The observation location's latitude as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Latitude_String() As String
            Get
                Return _latitudeString
            End Get
        End Property

        ''' <summary>
        ''' The observation location's longitude (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Longitude() As Double
            Get
                Return _longitude
            End Get
        End Property

        ''' <summary>
        ''' The display location's longitude as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Longitude_String() As String
            Get
                Return _longitudeString
            End Get
        End Property

        ''' <summary>
        ''' The observation location's elevation (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Elevation() As Integer
            Get
                Return _elevation
            End Get
        End Property

        ''' <summary>
        ''' The observation location's elevation as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ElevationString() As String
            Get
                Return _elevationString
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ObservationTime
        Private _observationTime As DateTime = Nothing
        Private _observationTimeString As String = "--"
        Private _observationTimeZoneShort As String = "--"
        Private _observationTimeZoneLong As String = "--"

        Public Sub New(ByVal timeString As String,
                       ByVal timeZoneShort As String,
                       ByVal timeZoneLong As String)

            If Not DateTime.TryParse(timeString, _observationTime) Then
                _observationTime = #1/1/1700#
                _observationTimeString = "N/A"
            Else
                _observationTimeString = _observationTime.ToString
            End If

            _observationTimeZoneShort = timeZoneShort
            _observationTimeZoneLong = timeZoneLong

        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The time that this observation was made (type DateTime).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ObservationTime() As DateTime
            Get
                Return _observationTime
            End Get
        End Property

        ''' <summary>
        ''' The time that this observation was made as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ObservationTime_String() As String
            Get
                Return _observationTimeString
            End Get
        End Property

        ''' <summary>
        ''' The time zone name that the observation was made (short format).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ObservationTimeZone_Short() As String
            Get
                Return _observationTimeZoneShort
            End Get
        End Property

        ''' <summary>
        ''' The time zone name that the observation was made (long format).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ObservationTimeZone_Long() As String
            Get
                Return _observationTimeZoneLong
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ForecastTime
        Private _forecastTime As DateTime = Nothing
        Private _forecastTimeString As String = "--"
        Private _forecastTimeZoneShort As String = "--"
        Private _forecastTimeZoneLong As String = "--"

        Public Sub New(ByVal timeString As String,
                       ByVal timeZoneShort As String,
                       ByVal timeZoneLong As String)

            If Not DateTime.TryParse(timeString, _forecastTime) Then
                _forecastTime = #1/1/1700#
                _forecastTimeString = "N/A"
            Else
                _forecastTimeString = _forecastTime.ToLongDateString
            End If

            _forecastTimeZoneShort = timeZoneShort
            _forecastTimeZoneLong = timeZoneLong

        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The time of this forecast (type DateTime).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastTime() As DateTime
            Get
                Return _forecastTime
            End Get
        End Property

        ''' <summary>
        ''' The time of this forecast as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastTime_String() As String
            Get
                Return _forecastTimeString
            End Get
        End Property

        ''' <summary>
        ''' The time zone name of this forecast (short format).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastTimeZone_Short() As String
            Get
                Return _forecastTimeZoneShort
            End Get
        End Property

        ''' <summary>
        ''' The time zone name of this forecast (long format).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastTimeZone_Long() As String
            Get
                Return _forecastTimeZoneLong
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Temperature
        Private _tempString As String = "--"
        Private _tempF As Double
        Private _tempC As Double
        Private _tempF_String As String = "--"
        Private _tempC_String As String = "--"

        Public Sub New(ByVal tempString As String,
                       ByVal tempF_String As String,
                       ByVal tempC_String As String)

            _tempString = tempString
            _tempF_String = tempF_String
            _tempC_String = tempC_String

            If Not Double.TryParse(_tempF_String, _tempF) Then
                _tempF = -1000000
                _tempF_String = "N/A"
            End If

            If Not Double.TryParse(_tempC_String, _tempC) Then
                _tempC = -1000000
                _tempC_String = "N/A"
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The dry-bulb temperature as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TemperatureString() As String
            Get
                Return _tempString
            End Get
        End Property

        ''' <summary>
        ''' The dry-bulb temperature in ° F. (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Temperature_Fahrenheit() As Double
            Get
                Return _tempF
            End Get
        End Property

        ''' <summary>
        ''' The dry-bulb temperature in ° F. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Temperature_Fahrenheit_String() As String
            Get
                Return _tempF_String & "° F."
            End Get
        End Property

        ''' <summary>
        ''' The dry-bulb temperature in ° C. (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Temperature_Celsius() As Double
            Get
                Return _tempC
            End Get
        End Property

        ''' <summary>
        ''' The dry-bulb temperature in ° C. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Temperature_Celsius_String() As String
            Get
                Return _tempC_String & "° C."
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RelativeHumidity
        Private _rhString As String = "--"
        Private _rhInteger As Integer
        Private _rhDouble As Double

        Public Sub New(ByVal rhString As String)

            _rhString = rhString

            If Not Integer.TryParse(_rhString.Replace("%", ""), _rhInteger) Then
                _rhInteger = -1000000
                _rhDouble = -1000000
                _rhString = "N/A"
            Else
                _rhString = _rhInteger.ToString("f0") & "% R.H."
                _rhDouble = _rhInteger / 100
            End If

        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The relative humidity as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property RelativeHumidity_String() As String
            Get
                Return _rhString
            End Get
        End Property

        ''' <summary>
        ''' The relative humidity (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property RH_Integer() As Integer
            Get
                Return _rhInteger
            End Get
        End Property

        ''' <summary>
        ''' The relative humidity (type Double). Note this is the integer value divided by 100.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property RH_Double() As Double
            Get
                Return _rhDouble
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Wind
        Public Enum WindDirectionOrdinal
            NotReported
            North
            North_NorthEast
            NorthEast
            East_NorthEast
            East
            East_SouthEast
            SouthEast
            South_SouthEast
            South
            South_SouthWest
            SouthWest
            West_SouthWest
            West
            West_NorthWest
            NorthWest
            North_NorthWest
        End Enum

        Private _windString As String = "--"
        Private _windDirString As String = "--"
        Private _windDirectionOrdinal As WindDirectionOrdinal = WindDirectionOrdinal.NotReported
        Private _windDirecitonOrdinalString As String = "--"
        Private _windDirectionDegrees As Integer
        Private _windDirectionDegreesString As String = "--"
        Private _windMPH As Double
        Private _windMPH_String As String = "--"
        Private _windKPH As Double
        Private _windKPH_String As String = "--"
        Private _windGustMPH As Double
        Private _windGustMPH_String As String = "--"
        Private _windGustKPH As Double
        Private _windGustKPH_String As String = "--"
        Private _windDirectionAndSpeed_MPH_String As String = "--"
        Private _windDirectionAndSpeed_KPH_String As String = "--"


        Public Sub New(ByVal windString As String,
                       ByVal windDirection As String,
                       ByVal windDegrees As String,
                       ByVal windMPH As String,
                       ByVal windMPH_Gusts As String,
                       ByVal windKPH As String,
                       ByVal windKPH_Gusts As String)

            _windString = windString

            If windString.ToLower.Trim = "calm" Then
                _windString = "Winds are calm"
                _windDirString = ""
                _windDirecitonOrdinalString = ""
                _windDirectionDegrees = 0
                _windDirectionDegreesString = ""
                _windMPH = 0
                _windMPH_String = ""
                _windKPH = 0
                _windKPH_String = ""
                _windGustMPH = 0
                _windGustMPH_String = ""
                _windGustKPH = 0
                _windGustKPH_String = ""
                _windDirectionAndSpeed_MPH_String = "Winds are calm"
                _windDirectionAndSpeed_KPH_String = "Winds are calm"
            Else
                _windDirString = windDirection

                Select Case windDirection
                    Case "N", "North"
                        _windDirectionOrdinal = WindDirectionOrdinal.North

                    Case "NNE", "North-northeast"
                        _windDirectionOrdinal = WindDirectionOrdinal.North_NorthEast

                    Case "NE", "Northeast"
                        _windDirectionOrdinal = WindDirectionOrdinal.NorthEast

                    Case "ENE", "East-northeast"
                        _windDirectionOrdinal = WindDirectionOrdinal.East_NorthEast

                    Case "E", "East"
                        _windDirectionOrdinal = WindDirectionOrdinal.East

                    Case "ESE", "East-southeast"
                        _windDirectionOrdinal = WindDirectionOrdinal.East_SouthEast

                    Case "SE", "Southeast"
                        _windDirectionOrdinal = WindDirectionOrdinal.SouthEast

                    Case "SSE", "South-southeast"
                        _windDirectionOrdinal = WindDirectionOrdinal.South_SouthEast

                    Case "S", "South"
                        _windDirectionOrdinal = WindDirectionOrdinal.South

                    Case "SSW", "South-southwest"
                        _windDirectionOrdinal = WindDirectionOrdinal.South_SouthWest

                    Case "SW", "Southwest"
                        _windDirectionOrdinal = WindDirectionOrdinal.SouthWest

                    Case "WSW", "West-southwest"
                        _windDirectionOrdinal = WindDirectionOrdinal.West_SouthWest

                    Case "W", "West"
                        _windDirectionOrdinal = WindDirectionOrdinal.West

                    Case "WNW", "West-northwest"
                        _windDirectionOrdinal = WindDirectionOrdinal.West_NorthWest

                    Case "NW", "Northwest"
                        _windDirectionOrdinal = WindDirectionOrdinal.NorthWest

                    Case "NNW", "North-northwest"
                        _windDirectionOrdinal = WindDirectionOrdinal.North_NorthWest
                End Select

                _windDirecitonOrdinalString = "Winds are from the " &
                    _windDirectionOrdinal.ToString.Replace("_"c, "-"c).ToLower

                _windDirectionDegreesString = windDegrees

                If Not Integer.TryParse(_windDirectionDegreesString, _windDirectionDegrees) Then
                    _windDirectionDegrees = -1000000
                    _windDirectionDegreesString = "N/A"
                Else
                    _windDirectionDegreesString = _windDirectionDegrees.ToString("f0") & "°"
                End If

                _windMPH_String = windMPH

                If Not Double.TryParse(_windMPH_String, _windMPH) Then
                    _windMPH = -1000000
                    _windMPH_String = "N/A"
                Else
                    _windMPH_String = _windMPH.ToString("f1") & " MPH"
                End If

                _windGustMPH_String = windMPH_Gusts

                If Not Double.TryParse(_windGustMPH_String, _windGustMPH) Then
                    _windGustMPH = -1000000
                    _windGustMPH_String = "N/A"
                Else
                    _windGustMPH_String = _windGustMPH.ToString("f1") & " MPH"
                End If

                _windKPH_String = windKPH

                If Not Double.TryParse(_windKPH_String, _windKPH) Then
                    _windKPH = -1000000
                    _windKPH_String = "N/A"
                Else
                    _windKPH_String = _windKPH.ToString("f1") & " KPH"
                End If

                _windGustKPH_String = windKPH_Gusts

                If Not Double.TryParse(_windGustKPH_String, _windGustKPH) Then
                    _windGustKPH = -1000000
                    _windGustKPH_String = "N/A"
                Else
                    _windGustKPH_String = _windGustKPH.ToString("f1") & " KPH"
                End If

                If _windGustMPH > 0 Then
                    _windDirectionAndSpeed_MPH_String = _windDirecitonOrdinalString & " at " &
                    _windMPH_String & " and gusting at " & _windGustMPH_String
                Else
                    _windDirectionAndSpeed_MPH_String = _windDirecitonOrdinalString & " at " &
                    _windMPH_String
                End If

                If _windGustKPH > 0 Then
                    _windDirectionAndSpeed_KPH_String = _windDirecitonOrdinalString & " at " &
                    _windKPH_String & " and gusting at " & _windGustKPH_String
                Else
                    _windDirectionAndSpeed_KPH_String = _windDirecitonOrdinalString & " at " &
                    _windKPH_String
                End If
            End If

        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The properties of the wind as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindString() As String
            Get
                Return _windString
            End Get
        End Property

        ''' <summary>
        ''' The ordinal direction of the wind as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindOrdinalDirection_String() As String
            Get
                Return _windDirecitonOrdinalString
            End Get
        End Property

        ''' <summary>
        ''' The wind direction expressed in degrees (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindDirectionDegrees() As Integer
            Get
                Return _windDirectionDegrees
            End Get
        End Property

        ''' <summary>
        ''' The wind direction expressed in degrees as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindDirectionDegrees_String() As String
            Get
                Return _windDirectionDegreesString
            End Get
        End Property

        ''' <summary>
        ''' The wind speed in MPH (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindSpeed_MPH() As Double
            Get
                Return _windMPH
            End Get
        End Property

        ''' <summary>
        ''' The wind speed in MPH as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindSpeed_MPH_String() As String
            Get
                Return _windMPH_String
            End Get
        End Property

        ''' <summary>
        ''' The wind speed in KPH (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindSpeed_KPH() As Double
            Get
                Return _windKPH
            End Get
        End Property

        ''' <summary>
        ''' The wind speed in KPH as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindSpeed_KPH_String() As String
            Get
                Return _windKPH_String
            End Get
        End Property

        ''' <summary>
        ''' The wind direction and speed in MPH (including wind gusts, if applicable) as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindDirectionAndSpeed_MPH_String() As String
            Get
                Return _windDirectionAndSpeed_MPH_String
            End Get
        End Property

        ''' <summary>
        ''' The wind direction and speed in KPH (including wind gusts, if applicable) as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindDirectionAndSpeed_KPH_String() As String
            Get
                Return _windDirectionAndSpeed_KPH_String
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Pressure
        Private _pressureMB As Integer
        Private _pressureMB_String As String = "--"
        Private _pressureInHg As Double
        Private _pressureInHg_String As String = "--"
        Private _pressureTrend As String = "--"

        Public Sub New(ByVal pressureMB As String,
                       ByVal pressureIn As String,
                       ByVal pressureTrend As String)

            _pressureMB_String = pressureMB

            If Not Integer.TryParse(_pressureMB_String, _pressureMB) Then
                _pressureMB = -1000000
                _pressureMB_String = "N/A"
            Else
                _pressureMB_String = _pressureMB.ToString("f0") & " mb."
            End If

            _pressureInHg_String = pressureIn

            If Not Double.TryParse(_pressureInHg_String, _pressureInHg) Then
                _pressureInHg = -1000000
                _pressureInHg_String = "N/A"
            Else
                _pressureInHg_String = _pressureInHg.ToString("f2") & Chr(34) & " Hg"
            End If

            _pressureTrend = pressureTrend
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The barometric pressure in millibars (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PressureMillibars() As Integer
            Get
                Return _pressureMB
            End Get
        End Property

        ''' <summary>
        ''' The barometric pressure in millibars as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PressureMillibars_String() As String
            Get
                Return _pressureMB_String
            End Get
        End Property

        ''' <summary>
        ''' The barometric pressure in inches of mercury (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PressureInHg() As Double
            Get
                Return _pressureInHg
            End Get
        End Property

        ''' <summary>
        ''' The barometric pressure in inches of mercury as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PressureInHg_String() As String
            Get
                Return _pressureInHg_String
            End Get
        End Property

        ''' <summary>
        ''' A symbol indicating the trend of the barometric pressure.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PressureTrend() As String
            Get
                Return _pressureTrend
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DewPoint
        Private _dpString As String = "--"
        Private _dpF As Integer
        Private _dpC As Integer
        Private _dpF_String As String = "--"
        Private _dpC_String As String = "--"

        Public Sub New(ByVal dpString As String,
                       ByVal dpF As String,
                       ByVal dpC As String)

            _dpString = dpString
            _dpF_String = dpF

            If Not Integer.TryParse(_dpF_String, _dpF) Then
                _dpF = -1000000
                _dpF_String = "N/A"
            Else
                _dpF_String = _dpF.ToString("f0") & "° F."
            End If

            _dpC_String = dpC

            If Not Integer.TryParse(_dpC_String, _dpC) Then
                _dpC = -1000000
                _dpC_String = "N/A"
            Else
                _dpC_String = _dpC.ToString("f0") & "° C."
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The dewpoint temperature as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DewPointString() As String
            Get
                Return _dpString
            End Get
        End Property

        ''' <summary>
        ''' The dewpoint temperature in ° F. (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DewPoint_Fahrenheit() As Integer
            Get
                Return _dpF
            End Get
        End Property

        ''' <summary>
        ''' The dewpoint temperature in ° F. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DewPoint_Fahrenheit_String() As String
            Get
                Return _dpF_String
            End Get
        End Property

        ''' <summary>
        ''' The dewpoint temperature in ° C. (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DewPoint_Celsius() As Integer
            Get
                Return _dpC
            End Get
        End Property

        ''' <summary>
        ''' The dewpoint temperature in ° C. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property DewPoint_Celsius_String() As String
            Get
                Return _dpC_String
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class HeatIndex
        Private _hiString As String = "--"
        Private _hiF As Double
        Private _hiF_String As String = "--"
        Private _hiC As Double
        Private _hiC_String As String = "--"

        Public Sub New(ByVal heatIndexString As String,
                       ByVal heatIndexF As String,
                       ByVal heatIndexC As String)

            _hiString = heatIndexString

            _hiF_String = heatIndexF

            If Not Double.TryParse(_hiF_String, _hiF) Then
                _hiF = -1000000
                _hiF_String = "N/A"
            Else
                _hiF_String = _hiF.ToString("f1") & "° F."
            End If

            _hiC_String = heatIndexC

            If Not Double.TryParse(_hiC_String, _hiC) Then
                _hiC = -1000000
                _hiC_String = "N/A"
            Else
                _hiC_String = _hiC.ToString("f1") & "° C."
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The heat index as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HeatIndexString() As String
            Get
                Return _hiString
            End Get
        End Property

        ''' <summary>
        ''' The heat index in ° F. (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HeatIndex_Fahrenheit() As Double
            Get
                Return _hiF
            End Get
        End Property

        ''' <summary>
        ''' The heat index in ° F. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HeatIndex_Fahrenheit_String() As String
            Get
                Return _hiF_String
            End Get
        End Property

        ''' <summary>
        ''' The heat index in ° C. (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HeatIndex_Celsius() As Double
            Get
                Return _hiC
            End Get
        End Property

        ''' <summary>
        ''' he heat index in ° C. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property HeatIndex_Celsius_String() As String
            Get
                Return _hiC_String
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WindChill
        Private _wcString As String = "--"
        Private _wcF As Double
        Private _wcF_String As String = "--"
        Private _wcC As Double
        Private _wcC_String As String = "--"

        Public Sub New(ByVal wcString As String,
                       ByVal wcF As String,
                       ByVal wcC As String)

            _wcString = wcString

            _wcF_String = wcF

            If Not Double.TryParse(_wcF_String, _wcF) Then
                _wcF = -1000000
                _wcF_String = "N/A"
            Else
                _wcF_String = _wcF.ToString("f1") & "° F."
            End If

            _wcC_String = wcC

            If Not Double.TryParse(_wcC_String, _wcC) Then
                _wcC = -1000000
                _wcC_String = "N/A"
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The windchill as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindChillString() As String
            Get
                Return _wcString
            End Get
        End Property

        ''' <summary>
        ''' The windchill in ° F. (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindChill_Fahrenheit() As Double
            Get
                Return _wcF
            End Get
        End Property

        ''' <summary>
        ''' The windchill in ° F. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindChill_Fahrenheit_String() As String
            Get
                Return _wcF_String
            End Get
        End Property

        ''' <summary>
        ''' The windchill in ° C. (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindChill_Celsius() As Double
            Get
                Return _wcC
            End Get
        End Property

        ''' <summary>
        ''' The windchill in ° C. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindChill_Celsius_String() As String
            Get
                Return _wcC_String
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FeelsLikeTemperature
        Private _fltString As String = "--"
        Private _fltF As Double
        Private _fltF_String As String = "--"
        Private _fltC As Double
        Private _fltC_String As String = "--"

        Public Sub New(ByVal feelsLike As String,
                       ByVal feelsLikeF As String,
                       ByVal feelsLikeC As String)

            _fltString = feelsLike

            _fltF_String = feelsLikeF

            If Not Double.TryParse(_fltF_String, _fltF) Then
                _fltF = -1000000
                _fltF_String = "N/A"
            Else
                _fltF_String = _fltF.ToString("f0") & "° F."
            End If

            _fltC_String = feelsLikeC

            If Not Double.TryParse(_fltC_String, _fltC) Then
                _fltC = -1000000
                _fltC_String = "N/A"
            Else
                _fltC_String = _fltC.ToString("f0") & "° C."
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The "Feels Like" temperature as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FeelsLikeString() As String
            Get
                Return _fltString
            End Get
        End Property

        ''' <summary>
        ''' The "Feels Like" temperature in ° F. (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FeelsLike_Fahrenheit() As Double
            Get
                Return _fltF
            End Get
        End Property

        ''' <summary>
        ''' The "Feels Like" temperature in ° F. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FeelsLike_Fahrenheit_String() As String
            Get
                Return _fltF_String
            End Get
        End Property

        ''' <summary>
        ''' The "Feels Like" temperature in ° C. (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FeelsLike_Celsius() As Double
            Get
                Return _fltC
            End Get
        End Property

        ''' <summary>
        ''' The "Feels Like" temperature in ° C. as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FeelsLike_Celsius_String() As String
            Get
                Return _fltC_String
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Visibility
        Private _visMiles As Double
        Private _visMilesString As String = "--"
        Private _visKilometers As Double
        Private _visKilometersString As String = "--"

        Public Sub New(ByVal miles As String,
                       ByVal kilometers As String)

            If Not Double.TryParse(miles, _visMiles) Then
                _visMiles = -1000000
                _visMilesString = "N/A"
            Else
                _visMilesString = _visMiles.ToString("f0") & " Miles"
            End If

            If Not Double.TryParse(kilometers, _visKilometers) Then
                _visKilometers = -1000000
                _visKilometersString = "N/A"
            Else
                _visKilometersString = _visKilometers.ToString("f1") & " Kilometers"
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The opacity of the atmosphere in miles (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Visibility_Miles() As Double
            Get
                Return _visMiles
            End Get
        End Property

        ''' <summary>
        ''' The opacity of the atmosphere in miles as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Visibility_Miles_String() As String
            Get
                Return _visMilesString
            End Get
        End Property

        ''' <summary>
        ''' The opacity of the atmosphere in kilometers (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Visibility_Kilometers() As Double
            Get
                Return _visKilometers
            End Get
        End Property

        ''' <summary>
        ''' The opacity of the atmosphere in kilometers as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Visibility_Kilometers_String() As String
            Get
                Return _visKilometersString
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Precipitation
        Private _precipOneHour_In As Double
        Private _precipOneHour_In_String As String = "--"
        Private _precipOneHour_MM As Double
        Private _precipOneHour_MM_String As String = "--"
        Private _precipToday_In As Double
        Private _precipToday_In_String As String = "--"
        Private _precipToday_MM As Double
        Private _precipToday_MM_String As String = "--"

        Public Sub New(ByVal oneHourIn As String,
                       ByVal oneHourMM As String,
                       ByVal todayIn As String,
                       ByVal todayMM As String)

            If Not Double.TryParse(oneHourIn, _precipOneHour_In) Then
                _precipOneHour_In = -1000000
                _precipOneHour_In_String = "N/A"

            Else
                If _precipOneHour_In = -9999 Then
                    _precipOneHour_In = 0
                End If

                _precipOneHour_In_String = _precipOneHour_In.ToString("f2") & Chr(34)
            End If

            If Not Double.TryParse(oneHourMM, _precipOneHour_MM) Then
                _precipOneHour_MM = -1000000
                _precipOneHour_MM_String = "N/A"
            Else
                If _precipOneHour_MM = -9999 Then
                    _precipOneHour_MM = 0
                End If

                _precipOneHour_MM_String = _precipOneHour_MM.ToString("f0") & " mm"
            End If

            If Not Double.TryParse(todayIn, _precipToday_In) Then
                _precipToday_In = -1000000
                _precipToday_In_String = "N/A"
            Else
                _precipToday_In_String = _precipToday_In.ToString("f2") & Chr(34)
            End If

            If Not Double.TryParse(todayMM, _precipToday_MM) Then
                _precipToday_MM = -1000000
                _precipToday_MM_String = "N/A"
            Else
                _precipToday_MM_String = _precipToday_MM.ToString("f0") & " mm"
            End If

            If _precipOneHour_In < 0 Or _precipOneHour_MM < 0 Then
                _precipOneHour_In = 0
                _precipOneHour_In_String = "None"
                _precipOneHour_MM = 0
                _precipOneHour_MM_String = "None"
            End If

            If _precipToday_In < 0 Or _precipToday_MM < 0 Then
                _precipToday_In = 0
                _precipToday_In_String = "None"
                _precipToday_MM = 0
                _precipToday_MM_String = "None"
            End If

        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The one-hour precipitation in inches (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property OneHourPrecipitation_Inches() As Double
            Get
                Return _precipOneHour_In
            End Get
        End Property

        ''' <summary>
        ''' The one-hour precipitation in inches as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property OneHourPrecipition_Inches_String() As String
            Get
                Return _precipOneHour_In_String
            End Get
        End Property

        ''' <summary>
        ''' The one-hour precipitation in millimeters (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property OneHourPrecipitation_MM() As Double
            Get
                Return _precipOneHour_MM
            End Get
        End Property

        ''' <summary>
        ''' The one-hour precipitation in millimeters as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property OneHourPrecipitation_MM_String() As String
            Get
                Return _precipOneHour_MM_String
            End Get
        End Property

        ''' <summary>
        ''' The total precipitation for today in inches (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TodayPrecipitation_Inches() As Double
            Get
                Return _precipToday_In
            End Get
        End Property

        ''' <summary>
        ''' The total precipitation for today in inches as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TodayPrecipition_Inches_String() As String
            Get
                Return _precipToday_In_String
            End Get
        End Property

        ''' <summary>
        ''' The total precipitation for today in millimeters (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TodayPrecipitation_MM() As Double
            Get
                Return _precipToday_MM
            End Get
        End Property

        ''' <summary>
        ''' The total precipitation for today in millimeters as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property TodayPrecipitation_MM_String() As String
            Get
                Return _precipToday_MM_String
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class QuantitativePrecipitationForecast
        Private _in As Double
        Private _inString As String = "--"
        Private _mm As Double
        Private _mmString As String = "--"

        Public Sub New(ByVal inches As String,
                       ByVal mm As String)

            If Not Double.TryParse(inches, _in) Then
                _in = -1000000
                _inString = "N/A"
            Else
                _inString = _in.ToString("f2") & Chr(34)
            End If

            If Not Double.TryParse(mm, _mm) Then
                _mm = -1000000
                _mmString = "N/A"
            Else
                _mmString = _mm.ToString("f1") & " mm"
            End If

        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The expected amount of melted precipitation in inches accumulated over a three-hour period (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property QPF_Inches() As Double
            Get
                Return _in
            End Get
        End Property

        ''' <summary>
        ''' The expected amount of melted precipitation in inches accumulated over a three-hour period as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property QPF_Inches_String() As String
            Get
                Return _inString
            End Get
        End Property

        ''' <summary>
        ''' The expected amount of melted precipitation in millimeters accumulated over a three-hour period (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property QPF_MM() As Double
            Get
                Return _mm
            End Get
        End Property

        ''' <summary>
        ''' The expected amount of melted precipitation in millimeters accumulated over a three-hour period as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property QPF_MM_String() As String
            Get
                Return _mmString
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SnowForecast
        Private _in As Double
        Private _inString As String = "--"
        Private _cm As Double
        Private _cmString As String = "--"

        Public Sub New(ByVal inches As String,
                      ByVal cm As String)

            If Not Double.TryParse(inches, _in) Then
                _in = -1000000
                _inString = "N/A"
            Else
                _inString = _in.ToString("f2") & Chr(34)
            End If

            If Not Double.TryParse(cm, _cm) Then
                _cm = -1000000
                _cmString = "N/A"
            Else
                _cmString = _cm.ToString("f1") & " cm"
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The expected amount of snowfall in inches accumulated over a three-hour period (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SnowForecast_Inches() As Double
            Get
                Return _in
            End Get
        End Property

        ''' <summary>
        ''' The expected amount of snowfall in inches accumulated over a three-hour period as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SnowForecast_Inches_String() As String
            Get
                Return _inString
            End Get
        End Property

        ''' <summary>
        ''' The expected amount of snowfall in centimeters accumulated over a three-hour period (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SnowForecast_CM() As Double
            Get
                Return _cm
            End Get
        End Property

        ''' <summary>
        ''' The expected amount of snowfall in centimeters accumulated over a three-hour period as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SnowForecast_CM_String() As String
            Get
                Return _cmString
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WindForecast
        Public Enum WindDirectionOrdinal
            NotReported
            North
            North_NorthEast
            NorthEast
            East_NorthEast
            East
            East_SouthEast
            SouthEast
            South_SouthEast
            South
            South_SouthWest
            SouthWest
            West_SouthWest
            West
            West_NorthWest
            NorthWest
            North_NorthWest
        End Enum

        Private _windFormattedStringMPH As String = "--"
        Private _windFormattedStringKPH As String = "--"
        Private _windDirectionOrdinal As WindDirectionOrdinal = WindDirectionOrdinal.NotReported
        Private _windDirecitonOrdinalString As String = "--"
        Private _windDirectionDegrees As Integer
        Private _windDirectionDegreesString As String = "--"
        Private _windMPH As Double
        Private _windMPH_String As String = "--"
        Private _windKPH As Double
        Private _windKPH_String As String = "--"

        Public Sub New(ByVal mph As String,
                       ByVal kph As String,
                       ByVal dir As String,
                       ByVal degrees As String)

            Select Case dir
                Case "N", "North"
                    _windDirectionOrdinal = WindDirectionOrdinal.North

                Case "NNE", "North-northeast"
                    _windDirectionOrdinal = WindDirectionOrdinal.North_NorthEast

                Case "NE", "Northeast"
                    _windDirectionOrdinal = WindDirectionOrdinal.NorthEast

                Case "ENE", "East-northeast"
                    _windDirectionOrdinal = WindDirectionOrdinal.East_NorthEast

                Case "E", "East"
                    _windDirectionOrdinal = WindDirectionOrdinal.East

                Case "ESE", "East-southeast"
                    _windDirectionOrdinal = WindDirectionOrdinal.East_SouthEast

                Case "SE", "Southeast"
                    _windDirectionOrdinal = WindDirectionOrdinal.SouthEast

                Case "SSE", "South-southeast"
                    _windDirectionOrdinal = WindDirectionOrdinal.South_SouthEast

                Case "S", "South"
                    _windDirectionOrdinal = WindDirectionOrdinal.South

                Case "SSW", "South-southwest"
                    _windDirectionOrdinal = WindDirectionOrdinal.South_SouthWest

                Case "SW", "Southwest"
                    _windDirectionOrdinal = WindDirectionOrdinal.SouthWest

                Case "WSW", "West-southwest"
                    _windDirectionOrdinal = WindDirectionOrdinal.West_SouthWest

                Case "W", "West"
                    _windDirectionOrdinal = WindDirectionOrdinal.West

                Case "WNW", "West-northwest"
                    _windDirectionOrdinal = WindDirectionOrdinal.West_NorthWest

                Case "NW", "Northwest"
                    _windDirectionOrdinal = WindDirectionOrdinal.NorthWest

                Case "NNW", "North-northwest"
                    _windDirectionOrdinal = WindDirectionOrdinal.North_NorthWest
            End Select

            _windDirecitonOrdinalString = "Winds will be from the " &
                _windDirectionOrdinal.ToString.Replace("_"c, "-"c).ToLower

            _windDirectionDegreesString = degrees

            If Not Integer.TryParse(_windDirectionDegreesString, _windDirectionDegrees) Then
                _windDirectionDegrees = -1000000
                _windDirectionDegreesString = "N/A"
            Else
                _windDirectionDegreesString = _windDirectionDegrees.ToString("f0") & "°"
            End If

            _windMPH_String = mph

            If Not Double.TryParse(mph, _windMPH) Then
                _windMPH = -1000000
                _windMPH_String = "N/A"
            Else
                _windMPH_String = _windMPH.ToString("f1") & " MPH"
            End If

            If Not Double.TryParse(kph, _windKPH) Then
                _windKPH = -1000000
                _windKPH_String = "N/A"
            Else
                _windKPH_String = _windKPH.ToString("f1") & " KPH"
            End If

            _windFormattedStringMPH = _windDirecitonOrdinalString & " at " & _windMPH_String
            _windFormattedStringKPH = _windDirecitonOrdinalString & " at " & _windKPH_String
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The wind speed in MPH as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindFormattedMPH_String() As String
            Get
                Return _windFormattedStringMPH
            End Get
        End Property

        ''' <summary>
        ''' The wind speed in KPH as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindFormattedKPH_String() As String
            Get
                Return _windFormattedStringKPH
            End Get
        End Property

        ''' <summary>
        ''' The ordinal direction of the wind as a formatted string (2 of 2).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindOrdinalDirection_String() As String
            Get
                Return _windDirecitonOrdinalString
            End Get
        End Property
        ''' <summary>
        ''' The wind direction expressed in degrees (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindDirectionDegrees() As Integer
            Get
                Return _windDirectionDegrees
            End Get
        End Property

        ''' <summary>
        ''' The wind direction expressed in degrees as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindDirectionDegreesString() As String
            Get
                Return _windDirectionDegreesString
            End Get
        End Property

        ''' <summary>
        ''' The wind speed in MPH (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindSpeed_MPH() As Double
            Get
                Return _windMPH
            End Get
        End Property

        ''' <summary>
        ''' The wind speed in MPH as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindSpeed_MPH_String() As String
            Get
                Return _windMPH_String
            End Get
        End Property

        ''' <summary>
        ''' The wind speed in KPH (type Double).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindSpeed_KPH() As Double
            Get
                Return _windKPH
            End Get
        End Property

        ''' <summary>
        ''' The wind speed in KPH as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WindSpeed_KPH_String() As String
            Get
                Return _windKPH_String
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ProbabilityOfPrecipitation
        Private _popString As String = "--"
        Private _popInteger As Integer
        Private _popDouble As Double

        Public Sub New(ByVal popString As String)

            If Not Integer.TryParse(popString, _popInteger) Then
                _popInteger = -1000000
                _popDouble = -1000000
                _popString = "N/A"
            Else
                _popDouble = _popInteger / 100
                _popString = _popInteger.ToString("f0") & "% Chance"
            End If

        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The probabilty (percent) of precipitation (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ProbabilityOfPrecipitation_Integer() As Integer
            Get
                Return _popInteger
            End Get
        End Property

        ''' <summary>
        ''' The probabilty (percent) of precipitation (type Double). Note this is the integer value divided by 100.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ProbabilityOfPrecipitation_Double() As Double
            Get
                Return _popDouble
            End Get
        End Property

        ''' <summary>
        ''' The probabilty (percent) of precipitation as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ProbabilityOfPrecipitation_String() As String
            Get
                Return _popString
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class WeatherIcon
        Private _iconText As String = "--"
        Private _iconURL_k As String = "--"

        Private _iconURL_a As String = "--"
        Private _iconURL_b As String = "--"
        Private _iconURL_c As String = "--"
        Private _iconURL_d As String = "--"
        Private _iconURL_e As String = "--"
        Private _iconURL_f As String = "--"
        Private _iconURL_g As String = "--"
        Private _iconURL_h As String = "--"
        Private _iconURL_i As String = "--"
        Private _iconURL_j As String = "--"

        Public Sub New(ByVal text As String,
                       ByVal url As String)

            _iconText = text
            _iconURL_k = url

            _iconURL_a = _iconURL_k.Replace("/k/", "/a/")
            _iconURL_b = _iconURL_k.Replace("/k/", "/b/")
            _iconURL_c = _iconURL_k.Replace("/k/", "/c/")
            _iconURL_d = _iconURL_k.Replace("/k/", "/d/")
            _iconURL_e = _iconURL_k.Replace("/k/", "/e/")
            _iconURL_f = _iconURL_k.Replace("/k/", "/f/")
            _iconURL_g = _iconURL_k.Replace("/k/", "/g/")
            _iconURL_h = _iconURL_k.Replace("/k/", "/h/")
            _iconURL_i = _iconURL_k.Replace("/k/", "/i/")
            _iconURL_j = _iconURL_k.Replace("/k/", "/j/")
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The text of the icon used.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconText() As String
            Get
                Return _iconText
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 1 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set01() As String
            Get
                Return _iconURL_a
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 2 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set02() As String
            Get
                Return _iconURL_b
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 3 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set03() As String
            Get
                Return _iconURL_c
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 4 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set04() As String
            Get
                Return _iconURL_d
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 5 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set05() As String
            Get
                Return _iconURL_e
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 6 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set06() As String
            Get
                Return _iconURL_f
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 7 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set07() As String
            Get
                Return _iconURL_g
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 8 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set08() As String
            Get
                Return _iconURL_h
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 9 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set09() As String
            Get
                Return _iconURL_i
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 10 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set10() As String
            Get
                Return _iconURL_j
            End Get
        End Property

        ''' <summary>
        ''' The URL of the icon appropriate for the condition (set 11 of 11). I hope to be adding to these sets! :)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IconURL_Set11() As String
            Get
                Return _iconURL_k
            End Get
        End Property

    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ForecastData_ExtendedInfo
        Private _forecastPeriod As ForecastTime = Nothing
        Private _forecastConditions As String = "--"
        Private _forecastPOP As ProbabilityOfPrecipitation = Nothing
        Private _forecastHighTemp As Temperature = Nothing
        Private _forecastLowTemp As Temperature = Nothing
        Private _forecastIcon As WeatherIcon = Nothing
        Private _qpfAllDay As QuantitativePrecipitationForecast = Nothing
        Private _qpfDay As QuantitativePrecipitationForecast = Nothing
        Private _qpfNight As QuantitativePrecipitationForecast = Nothing
        Private _snowAllDay As SnowForecast = Nothing
        Private _snowDay As SnowForecast = Nothing
        Private _snowNight As SnowForecast = Nothing
        Private _maxWind As WindForecast = Nothing
        Private _aveWind As WindForecast = Nothing
        Private _aveHumidity As RelativeHumidity = Nothing
        Private _maxHumidity As RelativeHumidity = Nothing
        Private _minHumidity As RelativeHumidity = Nothing

        Public Sub New(ByVal fPeriod As ForecastTime,
                       ByVal fConditions As String,
                       ByVal fPOP As ProbabilityOfPrecipitation,
                       ByVal fHighTemp As Temperature,
                       ByVal fLowTemp As Temperature,
                       ByVal fIcon As WeatherIcon,
                       ByVal qpfAllDay As QuantitativePrecipitationForecast,
                       ByVal qpfDay As QuantitativePrecipitationForecast,
                       ByVal qpfNight As QuantitativePrecipitationForecast,
                       ByVal snowAllDay As SnowForecast,
                       ByVal snowDay As SnowForecast,
                       ByVal snowNight As SnowForecast,
                       ByVal maxWind As WindForecast,
                       ByVal aveWind As WindForecast,
                       ByVal aveHumid As RelativeHumidity,
                       ByVal maxHumid As RelativeHumidity,
                       ByVal minHumid As RelativeHumidity)

            _forecastPeriod = fPeriod
            _forecastConditions = fConditions
            _forecastPOP = fPOP
            _forecastHighTemp = fHighTemp
            _forecastLowTemp = fLowTemp
            _forecastIcon = fIcon
            _qpfAllDay = qpfAllDay
            _qpfDay = qpfDay
            _qpfNight = qpfNight
            _snowAllDay = snowAllDay
            _snowDay = snowDay
            _snowNight = snowNight
            _maxWind = maxWind
            _aveWind = aveWind
            _aveHumidity = aveHumid
            _maxHumidity = maxHumid
            _minHumidity = minHumid
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The date and time for which this forecast is applicable.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastPeriod() As ForecastTime
            Get
                Return _forecastPeriod
            End Get
        End Property

        ''' <summary>
        ''' The predicted conditions for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastConditions() As String
            Get
                Return _forecastConditions
            End Get
        End Property

        ''' <summary>
        ''' The predicted probability of precipitation for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastProbabilityOfPrecipitation() As ProbabilityOfPrecipitation
            Get
                Return _forecastPOP
            End Get
        End Property

        ''' <summary>
        ''' The predicted high temperature (dry-bulb) for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastHighTemperature() As Temperature
            Get
                Return _forecastHighTemp
            End Get
        End Property

        ''' <summary>
        ''' The predicted low temperature (dry-bulb) for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastLowTemperature() As Temperature
            Get
                Return _forecastLowTemp
            End Get
        End Property

        ''' <summary>
        ''' Weather Icon(s) applicable for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastIcon() As WeatherIcon
            Get
                Return _forecastIcon
            End Get
        End Property

        ''' <summary>
        ''' The predicted quantitative precipitation (all day) for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property QPF_AllDay() As QuantitativePrecipitationForecast
            Get
                Return _qpfAllDay
            End Get
        End Property

        ''' <summary>
        ''' The predicted quantitative precipitation (during the day) for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property QPF_Day() As QuantitativePrecipitationForecast
            Get
                Return _qpfDay
            End Get
        End Property

        ''' <summary>
        ''' The predicted quantitative precipitation (during the night) for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property QPF_Night() As QuantitativePrecipitationForecast
            Get
                Return _qpfNight
            End Get
        End Property

        ''' <summary>
        ''' The predicted snowfall amount (all day) for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Snowfall_AllDay() As SnowForecast
            Get
                Return _snowAllDay
            End Get
        End Property

        ''' <summary>
        ''' The predicted snowfall amount (during the day) for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Snowfall_Day() As SnowForecast
            Get
                Return _snowDay
            End Get
        End Property

        ''' <summary>
        ''' The predicted snowfall amount (during the night) for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Snowfall_Night() As SnowForecast
            Get
                Return _snowNight
            End Get
        End Property

        ''' <summary>
        ''' The predicted maximum wind conditions for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property MaximumWind() As WindForecast
            Get
                Return _maxWind
            End Get
        End Property

        ''' <summary>
        ''' The predicted average wind conditions for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property AverageWind() As WindForecast
            Get
                Return _aveWind
            End Get
        End Property

        ''' <summary>
        ''' The predicted average relative humidity for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property AverageRH() As RelativeHumidity
            Get
                Return _aveHumidity
            End Get
        End Property

        ''' <summary>
        ''' The predicted maximum relative humidity for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property MaximumRH() As RelativeHumidity
            Get
                Return _maxHumidity
            End Get
        End Property

        ''' <summary>
        ''' The predicted minimum relative humidity for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property MinimumRH() As RelativeHumidity
            Get
                Return _minHumidity
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ForecastData_Text
        Private _weatherIcon As WeatherIcon = Nothing
        Private _title As String = "--"
        Private _forecastText_Imperial As String = "--"
        Private _forecastText_SI As String = "--"
        Private _pop As ProbabilityOfPrecipitation = Nothing

        Public Sub New(ByVal icon As WeatherIcon,
                       ByVal title As String,
                       ByVal imperial As String,
                       ByVal metric As String,
                       ByVal pop As ProbabilityOfPrecipitation)

            _weatherIcon = icon
            _title = title
            _forecastText_Imperial = imperial
            _forecastText_SI = metric
            _pop = pop
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The title to show for this forcast (e.g., "Wednesday", "Thursday Night", etc.).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Title() As String
            Get
                Return _title
            End Get
        End Property

        ''' <summary>
        ''' Weather Icon(s) applicable for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WeatherIcon() As WeatherIcon
            Get
                Return _weatherIcon
            End Get
        End Property

        ''' <summary>
        ''' The forecast text for this forecast period shown in imperial units.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastText_Imperial() As String
            Get
                Return _forecastText_Imperial
            End Get
        End Property

        ''' <summary>
        ''' The forecast text for this forecast period shown in metric units.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ForecastText_SI() As String
            Get
                Return _forecastText_SI
            End Get
        End Property

        ''' <summary>
        ''' The predicted probability of precipitation for this forecast period.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property ProbabilityOfPrecipitation() As ProbabilityOfPrecipitation
            Get
                Return _pop
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Sun
        Private _sunriseTime As DateTime
        Private _sunriseTime_String As String = "--"
        Private _sunsetTime As DateTime
        Private _sunsetTime_String As String = "--"

        Public Sub New(ByVal sunrise_Hour As String,
                       ByVal sunrise_Minute As String,
                       ByVal sunset_Hour As String,
                       ByVal sunset_Minute As String)

            Dim sunriseHourInt As Integer = 0
            Dim sunriseMinInt As Integer = 0

            If Not Integer.TryParse(sunrise_Hour, sunriseHourInt) Or
               Not Integer.TryParse(sunrise_Minute, sunriseMinInt) Then

                _sunriseTime = #1/1/1700#
                _sunriseTime_String = "N/A"
            Else
                _sunriseTime = New DateTime(Today.Year, Today.Month,
                                            Today.Day, sunriseHourInt,
                                            sunriseMinInt, 0)

                _sunriseTime_String = _sunriseTime.ToString("h:mm tt")
            End If

            Dim sunsetHourInt As Integer = 0
            Dim sunsetMinInt As Integer = 0

            If Not Integer.TryParse(sunset_Hour, sunsetHourInt) Or
               Not Integer.TryParse(sunset_Minute, sunsetMinInt) Then

                _sunsetTime = #1/1/1700#
                _sunsetTime_String = "N/A"
            Else
                _sunsetTime = New DateTime(Today.Year, Today.Month,
                                           Today.Day, sunsetHourInt,
                                           sunsetMinInt, 0)

                _sunsetTime_String = _sunsetTime.ToString("h:mm tt")
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The time of sunrise (type DateTime).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SunriseTime() As DateTime
            Get
                Return _sunriseTime
            End Get
        End Property

        ''' <summary>
        ''' The time of sunrise as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SunriseTime_String() As String
            Get
                Return _sunriseTime_String
            End Get
        End Property

        ''' <summary>
        ''' The time of sunset (type DateTime).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SunsetTime() As DateTime
            Get
                Return _sunsetTime
            End Get
        End Property

        ''' <summary>
        ''' The time of sunset as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property SunsetTime_String() As String
            Get
                Return _sunsetTime_String
            End Get
        End Property
    End Class





    ''' <summary>
    ''' This class isn't meant to be directly consumed; only the read-only properties of it are.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Moon
        Private _ageString As String = "--"
        Private _ageInt As Integer
        Private _pctIllumString As String = "--"
        Private _pctIllumInt As Integer
        Private _pctIllumDouble As Double

        Public Sub New(ByVal age As String,
                       ByVal pctIlluminated As String)

            If Not Integer.TryParse(age, _ageInt) Then
                _ageInt = -1000000
                _ageString = "N/A"
            Else
                If _ageInt = 1 Then
                    _ageString = _ageInt.ToString("f0") & " day since new moon"
                Else
                    _ageString = _ageInt.ToString("f0") & " days since new moon"
                End If
            End If

            If Not Integer.TryParse(pctIlluminated, _pctIllumInt) Then
                _pctIllumInt = -1000000
                _pctIllumDouble = -1000000
                _pctIllumString = "N/A"
            Else
                _pctIllumDouble = _pctIllumInt / 100
                _pctIllumString = _pctIllumInt.ToString("f0") & "%"
            End If
        End Sub

        Public Sub New()
        End Sub

        ''' <summary>
        ''' The age (days since the last new moon) of the moon's phase (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Age() As Integer
            Get
                Return _ageInt
            End Get
        End Property

        ''' <summary>
        ''' The age (days since the last new moon) of the moon's phase as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Age_String() As String
            Get
                Return _ageString
            End Get
        End Property

        ''' <summary>
        ''' The percentage of the moon which is illuminated for the moon's phase (type Integer).
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PercentIlluminated_Integer() As Integer
            Get
                Return _pctIllumInt
            End Get
        End Property

        ''' <summary>
        ''' The percentage of the moon which is illuminated for the moon's phase (type Integer). Note this is the integer value divided by 100.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PercentIlluminated_Double() As Double
            Get
                Return _pctIllumDouble
            End Get
        End Property

        ''' <summary>
        ''' The percentage of the moon which is illuminated for the moon's phase as a formatted string.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property PercentIlluminated_String() As String
            Get
                Return _pctIllumString
            End Get
        End Property
    End Class

#End Region

End Namespace