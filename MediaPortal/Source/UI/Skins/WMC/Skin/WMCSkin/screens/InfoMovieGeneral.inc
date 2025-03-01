<?xml version="1.0" encoding="utf-8"?>
<Grid
  Margin="40,10,20,10" IsVisible="{Binding ElementName=General, Path=IsChecked, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"
  xmlns="www.team-mediaportal.com/2008/mpf/directx"
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
  xmlns:fanart="clr-namespace:MediaPortal.Extensions.UserServices.FanArtService.Client;assembly=FanArtService.Client"
  xmlns:media="clr-namespace:MediaPortal.UiComponents.Media.Models.AspectWrappers;assembly=Media"
  xmlns:aspect="clr-namespace:MediaPortal.Common.MediaManagement.DefaultItemAspects;assembly=MediaPortal.Common"
  xmlns:mp_special_controls="clr-namespace:MediaPortal.UI.SkinEngine.SpecialElements.Controls;assembly=SkinEngine">

  <Grid.Resources>
    <fanart:FanArtImageSourceConverter x:Key="FanArtImageSourceConverter"/>
    <MultiAspectAttributeConverter x:Key="MultiAspectAttributeConverter"/>
  </Grid.Resources>
    <Grid.RowDefinitions>
      <RowDefinition Height="*"/>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
      <ColumnDefinition Width="*"/>
    </Grid.ColumnDefinitions>

    <StackPanel Grid.Column="0" Grid.Row="0" VerticalAlignment="Stretch" Orientation="Vertical" Margin="5,0,0,0">

      <media:MovieAspectWrapper MediaItem="{Binding MediaItem}" VerticalAlignment="Stretch">
        <media:MovieAspectWrapper.Template>
          <ControlTemplate>
            <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
              <Label x:Name="MediaItemName" Wrap="True" TextTrimming="WordEllipsis"
                     VerticalAlignment="Stretch" FontSize="{ThemeResource SmallFontSize}" FontFamily="Noto-Bold"
                     Content="{Binding MovieName}" Color="{ThemeResource TextColor}"/>
            </Grid>
          </ControlTemplate>
        </media:MovieAspectWrapper.Template>
      </media:MovieAspectWrapper>

      <media:MovieAspectWrapper MediaItem="{Binding MediaItem}" VerticalAlignment="Stretch"
                                IsVisible="{Binding Path=CollectionName,Converter={StaticResource ReferenceToBoolConverter}}">
        <media:MovieAspectWrapper.Template>
          <ControlTemplate>
            <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
              <Label x:Name="MediaItemName" Wrap="True" TextTrimming="WordEllipsis"
                     VerticalAlignment="Stretch" FontSize="{ThemeResource SmallFontSize}"
                     Content="{Binding CollectionName}" Color="{ThemeResource TextColor}"/>
            </Grid>
          </ControlTemplate>
        </media:MovieAspectWrapper.Template>
      </media:MovieAspectWrapper>

      <StackPanel Orientation="Horizontal">
        <media:MediaAspectWrapper MediaItem="{Binding MediaItem}" Margin="0,0,10,0">
          <media:MediaAspectWrapper.Template>
            <ControlTemplate>
              <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
                <Grid.RowDefinitions>
                  <RowDefinition Height="*"/>
                </Grid.RowDefinitions>
                <Grid.ColumnDefinitions>
                  <ColumnDefinition Width="*"/>
                </Grid.ColumnDefinitions>
                <Label x:Name="Year" HorizontalAlignment="Left" FontSize="{ThemeResource SmallFontSize}"
                    Content="{Binding Path=RecordingTime, Converter={StaticResource DateFormatConverter}, ConverterParameter=yyyy}" Color="{ThemeResource TextColor}"/>
              </Grid>
            </ControlTemplate>
          </media:MediaAspectWrapper.Template>
        </media:MediaAspectWrapper>
        <media:MovieAspectWrapper MediaItem="{Binding MediaItem}" VerticalAlignment="Center">
          <media:MovieAspectWrapper.Template>
            <ControlTemplate>
              <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
                <StackPanel Orientation="Horizontal" HorizontalAlignment="Left" VerticalAlignment="Center"
                            Margin="0,2,0,1">
                  <StarRatingPanel x:Name="RatingReadOnly" IsReadOnly="True" StarReadOnlyTemplate="{StaticResource StarReadOnlyTemplate}"
                                               Maximum="5" VerticalAlignment="Center" IsVisible="{Binding Path=TotalRating,Converter={StaticResource ReferenceToBoolConverter}}">
                    <StarRatingPanel.Value>
                      <MultiBinding Converter="{StaticResource ExpressionMultiValueConverter}" ConverterParameter="{}{0}/2">
                        <Binding Path="TotalRating"/>
                      </MultiBinding>
                    </StarRatingPanel.Value>
                    <StarRatingPanel.LayoutTransform>
                      <TransformGroup>
                        <ScaleTransform ScaleX="1.7" ScaleY="1.6"/>
                      </TransformGroup>
                    </StarRatingPanel.LayoutTransform>
                  </StarRatingPanel>
                  <Label x:Name="RatingCountLabel" Margin="15,2,5,1" VerticalAlignment="Center"
                         FontSize="{ThemeResource SmallerFontSize}" Opacity="1.3" Color="{ThemeResource MenuTextBackground}"
                         IsVisible="{Binding Path=TotalRating,Converter={StaticResource ReferenceToBoolConverter}}">
                    <Label.Content>
                      <Binding Path="TotalRating" Converter="{StaticResource StringFormatConverter}" ConverterParameter="{}({0:F1})"/>
                    </Label.Content>
                  </Label>
                </StackPanel>
              </Grid>
            </ControlTemplate>
          </media:MovieAspectWrapper.Template>
        </media:MovieAspectWrapper>
      </StackPanel>

      <Label x:Name="Genre" FontSize="{ThemeResource SmallFontSize}" TextTrimming="WordEllipsis"
             Opacity="1.3" Color="{ThemeResource MenuTextBackground}">
        <Label.Content>
          <MultiBinding Converter="{StaticResource MultiAspectAttributeConverter}">
            <Binding Path="MediaItem"/>
            <Binding Source="{x:Static aspect:GenreAspect.ATTR_GENRE}" />
          </MultiBinding>
        </Label.Content>
      </Label>

      <StackPanel Orientation="Horizontal">
        <media:MovieAspectWrapper MediaItem="{Binding MediaItem}">
          <media:MovieAspectWrapper.Template>
            <ControlTemplate>
              <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
                <StackPanel Orientation="Horizontal">
                  <Label x:Name="Duration" FontSize="{ThemeResource SmallFontSize}" Opacity="1.3" Color="{ThemeResource MenuTextBackground}"
                         IsVisible="{Binding Path=Runtime,Converter={StaticResource ReferenceToBoolConverter}}" Content="{Binding Path=Runtime}"/>
                  <Label Content=" min." FontSize="{ThemeResource SmallFontSize}" Opacity="1.3" Color="{ThemeResource MenuTextBackground}"
                         IsVisible="{Binding Path=Runtime,Converter={StaticResource ReferenceToBoolConverter}}"/>
                </StackPanel>
              </Grid>
            </ControlTemplate>
          </media:MovieAspectWrapper.Template>
        </media:MovieAspectWrapper>

        <media:MediaAspectWrapper MediaItem="{Binding MediaItem}">
          <media:MediaAspectWrapper.Template>
            <ControlTemplate>
              <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}">
                <Image HorizontalAlignment="Right" VerticalAlignment="Center" Stretch="Fill" Width="37" Height="24" Grid.Row="0" Grid.Column="1" Margin="0,0,20,0" >
                  <Image.Source>
                    <MultiBinding Converter="{StaticResource ExpressionMultiValueConverter}" ConverterParameter="{}{0} ? {1} : {2}">
                      <Binding Path="PlayCount"/>
                      <Binding Source=""/>
                      <Binding Source="unwatched_icon.png"/>
                    </MultiBinding>
                  </Image.Source>
                </Image>
              </Grid>
            </ControlTemplate>
          </media:MediaAspectWrapper.Template>
        </media:MediaAspectWrapper>
      </StackPanel>
    </StackPanel>

    <StackPanel Orientation="Horizontal" Grid.Row="1" HorizontalAlignment="Right" VerticalAlignment="Bottom" Margin="0,0,10,30">
      <media:MovieAspectWrapper MediaItem="{Binding MediaItem}" >
        <media:MovieAspectWrapper.Template>
          <ControlTemplate>
            <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}"
                  IsVisible="{Binding Path=Certification,Converter={StaticResource ReferenceToBoolConverter}}">
              <Grid.RowDefinitions>
                <RowDefinition Height="*"/>
              </Grid.RowDefinitions>
              <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*"/>
              </Grid.ColumnDefinitions>
              <Image Stretch="Uniform" Height="90" MaxWidth="165" Margin="0,0,20,0">
                <Image.Source>
                  <ImageSourceWrapper Delay="0" DelayInOut="false" VerticalTextureAlignment="Bottom" HorizontalTextureAlignment="Right">
                    <ImageSourceWrapper.Source>
                      <Binding Path="Certification" Converter="{StaticResource SafeFilenameConverter}" ConverterParameter="{}Certification\{0}.png"/>
                    </ImageSourceWrapper.Source>
                  </ImageSourceWrapper>
                </Image.Source>
              </Image>
            </Grid>
          </ControlTemplate>
        </media:MovieAspectWrapper.Template>
      </media:MovieAspectWrapper>

      <media:UserDataWrapper  MediaItem="{Binding MediaItem}">
        <media:UserDataWrapper.Template>
          <ControlTemplate>
            <Grid DataContext="{Binding RelativeSource={RelativeSource TemplatedParent}}"
                  IsVisible="{Binding Path=PlayPercentage,Converter={StaticResource ReferenceToBoolConverter}}">
              <Grid.RowDefinitions>
                <RowDefinition Height="*"/>
              </Grid.RowDefinitions>
              <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*"/>
              </Grid.ColumnDefinitions>
              <Image  Stretch="Fill" Width="90" Height="90" Margin="0,0,20,0">
                <Image.Source>
                  <Binding Path="PlayPercentage" Converter="{StaticResource StringFormatConverter}" ConverterParameter="{}CircularBar\{0:F0}.png"/>
                </Image.Source>
              </Image>
            </Grid>
          </ControlTemplate>
        </media:UserDataWrapper.Template>
      </media:UserDataWrapper>
    </StackPanel>


    <!--ScrollViewer HorizontalAlignment="Stretch" VerticalAlignment="Stretch"
        DockPanel.Dock="Center" HorizontalScrollBarVisibility="Disabled" Grid.Row="3" Grid.Column="0" Grid.ColumnSpan="2" Margin="20,10,20,5">
      <Label x:Name="StoryPlot" Wrap="True"
          HorizontalAlignment="Stretch" VerticalAlignment="Stretch"
          Content="{Binding VideoStoryPlot}"
          Color="{ThemeResource TextColor}"/>
    </ScrollViewer-->

</Grid>
