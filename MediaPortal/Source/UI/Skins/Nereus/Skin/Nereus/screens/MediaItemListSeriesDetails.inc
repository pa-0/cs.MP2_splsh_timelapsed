<?xml version="1.0" encoding="utf-8"?>
<Grid
  xmlns="www.team-mediaportal.com/2008/mpf/directx"
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
  xmlns:fanart="clr-namespace:MediaPortal.Extensions.UserServices.FanArtService.Client;assembly=FanArtService.Client"
  xmlns:media="clr-namespace:MediaPortal.UiComponents.Media.Models.AspectWrappers;assembly=Media"
  xmlns:aspect="clr-namespace:MediaPortal.Common.MediaManagement.DefaultItemAspects;assembly=MediaPortal.Common"
  Grid.Column="0" Grid.Row="0" x:Name="MediaItemDetails" Margin="0,220,20,-60"
  HorizontalAlignment="Stretch" VerticalAlignment="Stretch">

  <Grid.Resources>
    <fanart:FanArtImageSourceConverter x:Key="FanArtImageSourceConverter"/>
    <MultiAspectAttributeConverter x:Key="MultiAspectAttributeConverter"/>
  </Grid.Resources>
  <Grid.RowDefinitions>
    <RowDefinition Height="*"/>
  </Grid.RowDefinitions>
  <Grid.ColumnDefinitions>
    <ColumnDefinition Width="600"/>
    <ColumnDefinition Width="*"/>
  </Grid.ColumnDefinitions>

  <Grid Grid.Column="1" HorizontalAlignment="Right" VerticalAlignment="Bottom" Width="294" Height="428" Margin="0,-30,10,30">
    <Image HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Stretch="Fill" Width="294" Height="428">
      <Image.Source>
        <ImageSourceWrapper Delay="0.5" DelayInOut="true" Transition="transitions\dissolve" TransitionDuration="0.5"
                            Source="Poster_large.png"/>
      </Image.Source>
    </Image>
    <Image x:Name="SeriesPoster" HorizontalAlignment="Center" VerticalAlignment="Center" Stretch="Fill" Margin="8" Width="278" Height="412">
      <Image.Source>
        <ImageSourceWrapper Delay="0.5" DelayInOut="true" Transition="transitions\dissolve" TransitionDuration="0.5"
                            Source="{Binding Source={StaticResource FanArtBackgroundModel}, Path=ImageSource, Converter={StaticResource FanArtImageSourceConverter}, ConverterParameter=Poster;278;412}"/>
      </Image.Source>
    </Image>
    <Image HorizontalAlignment="Center" VerticalAlignment="Center" Stretch="Uniform" Width="200" Height="200"
           Source="video.png" IsVisible="{Binding ElementName=SeriesPoster, Path=!HasImage}"/>

    <media:SeriesAspectWrapper MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}" 
                               HorizontalAlignment="Right" VerticalAlignment="Top" Margin="0,-90,0,0"
                               IsVisible="{Binding Source={StaticResource SkinSettingsModel}, Path=[Nereus].EnableAgeCertificationLogos}">
      <media:SeriesAspectWrapper.Template>
        <ControlTemplate>
          <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}"
                IsVisible="{Binding Path=Certification,Converter={StaticResource ReferenceToBoolConverter}}">
            <Grid.RowDefinitions>
              <RowDefinition Height="*"/>
            </Grid.RowDefinitions>
            <Grid.ColumnDefinitions>
              <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions>
            <Image HorizontalAlignment="Right" VerticalAlignment="Top" Stretch="Uniform" Height="80" MaxWidth="150" Margin="0,-10,15,10">
              <Image.Source>
                <ImageSourceWrapper Delay="0" DelayInOut="false" VerticalTextureAlignment="Top" HorizontalTextureAlignment="Right">
                  <ImageSourceWrapper.Source>
                    <Binding Path="Certification" Converter="{StaticResource SafeFilenameConverter}" ConverterParameter="{}Certification\{0}.png"/>
                  </ImageSourceWrapper.Source>
                </ImageSourceWrapper>
              </Image.Source>
            </Image>
          </Grid>
        </ControlTemplate>
      </media:SeriesAspectWrapper.Template>
    </media:SeriesAspectWrapper>
    
  </Grid>

  <StackPanel Grid.ColumnSpan="2" Grid.Row="0" Margin="0,0,10,0" VerticalAlignment="Stretch">
    
    <media:SeriesAspectWrapper MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}" VerticalAlignment="Stretch">
      <media:SeriesAspectWrapper.Template>
        <ControlTemplate>
          <Label x:Name="SeriesNameLabel" Color="{ThemeResource TextColor}" FontSize="{ThemeResource NormalFontSize}" FontFamily="Noto-Bold" Wrap="True"
                 DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}" VerticalAlignment="Stretch">
            <Label.Content>
              <Binding Path="SeriesName"/>
            </Label.Content>
          </Label>
        </ControlTemplate>
      </media:SeriesAspectWrapper.Template>
    </media:SeriesAspectWrapper>
    
    <StackPanel Orientation="Horizontal">
      <media:SeriesAspectWrapper MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}">
        <media:SeriesAspectWrapper.Template>
          <ControlTemplate>
            <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}" VerticalAlignment="Center">
              <StackPanel Orientation="Horizontal" HorizontalAlignment="Left" VerticalAlignment="Center" Margin="0,3,0,3">
                <StarRatingPanel x:Name="RatingReadOnly" IsReadOnly="True" StarReadOnlyTemplate="{StaticResource StarReadOnlyTemplate}"
                                 Maximum="5" VerticalAlignment="Center" IsVisible="{Binding Path=TotalRating,Converter={StaticResource ReferenceToBoolConverter}}">
                  <StarRatingPanel.Value>
                    <MultiBinding Converter="{StaticResource ExpressionMultiValueConverter}" ConverterParameter="{}{0}/2">
                      <Binding Path="TotalRating"/>
                    </MultiBinding>
                  </StarRatingPanel.Value>
                  <StarRatingPanel.LayoutTransform>
                    <TransformGroup>
                      <ScaleTransform ScaleX="1.6" ScaleY="1.5"/>
                    </TransformGroup>
                  </StarRatingPanel.LayoutTransform>
                </StarRatingPanel>
                <Label x:Name="RatingCountLabel" Color="{ThemeResource TextColorForeground}" Margin="15,2,5,1"
                       FontSize="{ThemeResource SmallerFontSize}" IsVisible="{Binding Path=TotalRating,Converter={StaticResource ReferenceToBoolConverter}}">
                  <Label.Content>
                    <Binding Path="TotalRating" Converter="{StaticResource StringFormatConverter}" ConverterParameter="{}({0:F1})"/>
                  </Label.Content>
                </Label>
              </StackPanel>
            </Grid>
          </ControlTemplate>
        </media:SeriesAspectWrapper.Template>
      </media:SeriesAspectWrapper>
    </StackPanel>

    <Grid DataContext="{Binding Source={StaticResource FanArtBackgroundModel},Path=SelectedItem}">
      <Image Source="HomeContentIcons\series.png" Height="25" Width="25" Stretch="Uniform" Margin="2,0,5,0"
             VerticalAlignment="Center" HorizontalAlignment="Left"/>
      <StackPanel Orientation="Horizontal" Margin="40,0,0,0">
        <Label FontSize="{ThemeResource SmallFontSize}" Content="{Binding Seasons}"
               Margin="0,0,8,0" Color="{ThemeResource TextColorForeground}"/>
        <Label FontSize="{ThemeResource SmallFontSize}" Content="[Media.SeasonItemsHeader]"
               IsVisible="{Binding Path=Seasons,Converter={StaticResource ReferenceToBoolConverter}}"
               Margin="0,0,8,0" Color="{ThemeResource TextColorForeground}"/>
        <Label FontSize="{ThemeResource SmallerFontSize}" Content=" | " VerticalAlignment="Center"
               IsVisible="{Binding Path=Seasons,Converter={StaticResource ReferenceToBoolConverter}}"
               Margin="0,0,8,0" Color="{ThemeResource TextColorForeground}"/>
        <Label FontSize="{ThemeResource SmallFontSize}" Content="{Binding Episodes}"
               Margin="0,0,8,0" Color="{ThemeResource TextColorForeground}"/>
        <Label FontSize="{ThemeResource SmallFontSize}" Content="[Media.EpisodeItemsHeader]"
               IsVisible="{Binding Path=Episodes,Converter={StaticResource ReferenceToBoolConverter}}"
               Margin="0,0,8,0" Color="{ThemeResource TextColorForeground}"/>
      </StackPanel>
    </Grid>

    <Grid IsVisible="{Binding ElementName=Genre, Path=Content, Converter={StaticResource EmptyStringToFalseConverter}}">
      <Image Source="HomeContentIcons\genre.png" Height="30" Width="30" Stretch="Uniform" Margin="0,0,5,0"
             VerticalAlignment="Center" HorizontalAlignment="Left"/>
      <Label x:Name="Genre" FontSize="{ThemeResource SmallFontSize}" TextTrimming="WordEllipsis"
             Color="{ThemeResource TextColorForeground}" Margin="40,0,0,0">
        <Label.Content>
          <MultiBinding Converter="{StaticResource MultiAspectAttributeConverter}">
            <Binding Source="{StaticResource FanArtBackgroundModel}" Path="MediaItem"/>
            <Binding Source="{x:Static aspect:GenreAspect.ATTR_GENRE}" />
          </MultiBinding>
        </Label.Content>
      </Label>
    </Grid>

    <media:MediaAspectWrapper MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}" >
      <media:MediaAspectWrapper.Template>
        <ControlTemplate>
          <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
            <Image Source="HomeContentIcons\year.png" Height="27" Width="27" Stretch="Uniform" Margin="0,0,5,0" 
                   VerticalAlignment="Center" HorizontalAlignment="Left"/>
            <Label x:Name="Year" HorizontalAlignment="Left" FontSize="{ThemeResource SmallFontSize}" Color="{ThemeResource TextColorForeground}"
                   Content="{Binding Path=RecordingTime, Converter={StaticResource DateFormatConverter}, ConverterParameter=yyyy}" Margin="40,0,0,0"/>
          </Grid>
        </ControlTemplate>
      </media:MediaAspectWrapper.Template>
    </media:MediaAspectWrapper>

    <StackPanel Orientation="Horizontal">
        <media:UserDataWrapper MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}" Margin="30,-5,0,0">
          <media:UserDataWrapper.Template>
            <ControlTemplate>
              <Grid RenderTransformOrigin="0.5,0.5" DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
                <Grid.RenderTransform>
                  <TransformGroup>
                    <ScaleTransform ScaleX="0.7" ScaleY="0.7"/>
                  </TransformGroup>
                </Grid.RenderTransform>
                <Control Style="{ThemeResource CircularBackgroundStyle_0}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_18}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_36}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_54}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_72}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_90}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_108}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_126}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_144}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_162}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_180}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_198}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_216}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_234}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_252}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_270}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_288}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_306}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_324}"/>
                <Control Style="{ThemeResource CircularBackgroundStyle_342}"/>

                <Control Style="{ThemeResource CircularIndicatorStyle_0}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>0"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_18}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>5"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_36}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>10"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_54}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>15"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_72}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>20"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_90}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>25"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_108}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>30"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_126}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>35"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_144}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>40"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_162}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>45"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_180}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>50"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_198}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>55"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_216}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>60"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_234}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>65"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_252}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>70"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_270}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>75"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_288}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>80"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_306}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>85"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_324}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>90"/>
                  </Control.IsVisible>
                </Control>
                <Control Style="{ThemeResource CircularIndicatorStyle_342}">
                  <Control.IsVisible>
                    <Binding Path="PlayPercentage" Converter="{StaticResource ExpressionValueConverter}" ConverterParameter="{}{0}>95"/>
                  </Control.IsVisible>
                </Control>
                <Grid Margin="-70,-6,70,0">
                  <Grid.RowDefinitions>
                    <RowDefinition Height="*"/>
                  </Grid.RowDefinitions>
                  <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*"/>
                  </Grid.ColumnDefinitions>
                  <Label VerticalAlignment="Center" HorizontalAlignment="Center" FontSize="28" Color="White" FontFamily="Noto-Bold"
                         HorizontalContentAlignment="Center" VerticalContentAlignment="Center">
                    <Label.Content>
                      <Binding Path="PlayPercentage" Converter="{StaticResource StringFormatConverter}" ConverterParameter="{}{0:F0}%"/>
                    </Label.Content>
                  </Label>
                </Grid>
              </Grid>
            </ControlTemplate>
          </media:UserDataWrapper.Template>
        </media:UserDataWrapper>

      <media:SeriesAspectWrapper MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}"
                                 VerticalAlignment="Center" Margin="30,0,0,0">
        <media:SeriesAspectWrapper.Template>
          <ControlTemplate>
            <StackPanel Orientation="Vertical" DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}"
                        IsVisible="{Binding !IsEnded}">
              <StackPanel Orientation="Horizontal">
                <Label Content="Next Aired: " Color="{ThemeResource TextColor}" FontSize="{ThemeResource SmallFontSize}"
                       IsVisible="{Binding Path=NextAirDate,Converter={StaticResource ReferenceToBoolConverter}}"/>
                <Label VerticalAlignment="Stretch"
                       x:Name="NextAiredDate" Color="{ThemeResource TextColorForeground}" FontSize="{ThemeResource SmallFontSize}"
                       Content="{Binding Path=NextAirDate, Converter={StaticResource DateFormatConverter}, ConverterParameter=d}">
                </Label>
              </StackPanel>
              <Label x:Name="NextEpisode" Color="{ThemeResource TextColorForeground}" FontSize="{ThemeResource SmallFontSize}"
                     Wrap="True" Content="{Binding NextEpisodeName}" VerticalAlignment="Stretch">
              </Label>             
            </StackPanel>

          </ControlTemplate>
        </media:SeriesAspectWrapper.Template>
      </media:SeriesAspectWrapper>
      
      <Image HorizontalAlignment="Right" VerticalAlignment="Center" Stretch="Uniform" Height="140" Margin="60,-30,-280,-40">
        <Image.Source>
          <ImageSourceWrapper Delay="0.5" Source="{Binding Source={StaticResource FanArtBackgroundModel}, Path=ImageSource, Converter={StaticResource FanArtImageSourceConverter}, ConverterParameter=ClearArt;140;240}"/>
        </Image.Source>
      </Image>
    </StackPanel>

    <media:SeriesAspectWrapper MediaItem="{Binding Source={StaticResource FanArtBackgroundModel},Path=MediaItem}" VerticalAlignment="Stretch"
                               Margin="0,40,300,80">
      <media:SeriesAspectWrapper.Template>
        <ControlTemplate>
          <Grid VerticalAlignment="Stretch" MaxHeight="320">
            <Label x:Name="MediaItemDescription" DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}"
                   Wrap="True" TextTrimming="WordEllipsis" FontSize="{ThemeResource SmallFontSize}" Opacity="0.6"
                   VerticalAlignment="Stretch" HorizontalAlignment="Stretch" Content="{Binding Description}" Color="{ThemeResource TextColor}"/>
          </Grid>
        </ControlTemplate>
      </media:SeriesAspectWrapper.Template>
    </media:SeriesAspectWrapper>

  </StackPanel>
      
</Grid>
