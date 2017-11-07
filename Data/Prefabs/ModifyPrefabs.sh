#!/bin/sh


sed -i 's@<property name="YOffset" value="-8" />@<property name="YOffset" value="-12" />@g' *.xml
sed -i 's@<property name="ExcludeDistantPOIMesh" value="False" />@<property name="ExcludeDistantPOIMesh" value="False" />@g' *.xml


